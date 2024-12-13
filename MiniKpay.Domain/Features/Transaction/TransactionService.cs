using Microsoft.EntityFrameworkCore;
using MiniKpay.Database.Models;
using MiniKpay.Domain.Models;
using MiniKpay.Domain.Models.Transaction;

namespace MiniKpay.Domain.Features.Transaction
{
    public class TransactionService
    {
        private readonly AppDbContext _db;

        public TransactionService(AppDbContext db)
        {
            _db = db;
        }

        #region GetTransactionAsync

        public async Task<Result<List<TransactionModel>>> GetTransactionAsync()
        {
            Result<List<TransactionModel>> response;

            try
            {
                var transactions = _db.TblTransactions.AsNoTracking();

                if (transactions is null)
                {
                    return Result<List<TransactionModel>>.ValidationError("No transactions found.");
                }

                var lst = await transactions.Select(x=> new TransactionModel()
                {
                    TransferId = x.TransferId,
                    SenderMobileNo = x.SenderMobileNo,
                    ReceiverMobileNo = x.ReceiverMobileNo,
                    Amount = x.Amount,
                    Notes = x.Notes,
                }).ToListAsync();


                return Result<List<TransactionModel>>.Success(lst);
            }
            catch (Exception ex)
            {
                return Result<List<TransactionModel>>.SystemError(ex.Message);
            }
        }

        #endregion



        public async Task<Result<List<TransactionModel>>> GetTransactionByPhoneNumberAsync(string phoneNumber)
        {
            Result<List<TransactionModel>> response;

            try
            {
                var transactions = _db.TblTransactions
                    .Where(x => x.SenderMobileNo == phoneNumber)
                    .AsNoTracking();

                if (transactions is null)
                {
                    return Result<List<TransactionModel>>.ValidationError("No transactions found with this Phone Number.");
                }

                var lst = await transactions.Select(x => new TransactionModel()
                {
                    TransferId = x.TransferId,
                    SenderMobileNo = x.SenderMobileNo,
                    ReceiverMobileNo = x.ReceiverMobileNo,
                    Amount = x.Amount,
                    Notes = x.Notes,
                }).ToListAsync();


                return Result<List<TransactionModel>>.Success(lst);
            }
            catch (Exception ex)
            {
                return Result<List<TransactionModel>>.SystemError(ex.Message);
            }
        }

        public async Task<Result<TransactionRequestModel>> CreateTransactionAsync(TransactionRequestModel request)
        {
            try
            {
                if (request.Amount is null || request.Amount <= 0)
                {
                    return Result<TransactionRequestModel>.ValidationError("Transaction amount must be greater than 0.");
                }

                var sender = await _db.TblWallets.FirstOrDefaultAsync(x => x.MobileNumber == request.SenderMobileNo);
                var receiver = await _db.TblWallets.FirstOrDefaultAsync(x => x.MobileNumber == request.ReceiverMobileNo);

                if (request.SenderMobileNo == request.ReceiverMobileNo)
                {
                    return Result<TransactionRequestModel>.ValidationError("Sender and receiver phone numbers must be different.");
                }

                if (sender is null)
                {
                    return Result<TransactionRequestModel>.ValidationError("Sender phone number does not exist.");
                }

                if (receiver is null)
                {
                    return Result<TransactionRequestModel>.ValidationError("Receiver phone number does not exist.");
                }

                if (sender.Balance < request.Amount)
                {
                    return Result<TransactionRequestModel>.ValidationError("Insufficient balance.");
                }

                sender.Balance -= request.Amount.Value;  
                receiver.Balance += request.Amount.Value;

                _db.TblWallets.Update(sender);
                _db.TblWallets.Update(receiver);
                await _db.SaveChangesAsync();

                var transaction = new TblTransaction
                {
                    SenderMobileNo = request.SenderMobileNo,
                    ReceiverMobileNo = request.ReceiverMobileNo,
                    Amount = request.Amount.Value,  
                    Notes = request.Notes,
                };

                _db.TblTransactions.Add(transaction);
                await _db.SaveChangesAsync();

                return Result<TransactionRequestModel>.Success(request);
            }
            catch (Exception ex)
            {
                return Result<TransactionRequestModel>.SystemError($"An error occurred: {ex.Message}");
            }
        }



        //    var updatedSenderBalance = sender.Balance - transaction.Amount;
        //    var updatedReceiverBalance = receiver.Balance + transaction.Amount;

        //    int updateSenderResult = await _dA_Transaction.UpdateUserBalanceAsync(transaction.FromPhoneNumber, updatedSenderBalance);
        //    if (updateSenderResult == 0)
        //    {
        //        return Result<string>.SystemError(null, "Failed to update sender's balance.");
        //    }

        //    int updateReceiverResult = await _dA_Transaction.UpdateUserBalanceAsync(transaction.ToPhoneNumber, updatedReceiverBalance);
        //    if (updateReceiverResult == 0)
        //    {
        //        return Result<string>.SystemError(null, "Failed to update receiver's balance.");
        //    }

        //    int insertResult = await _dA_Transaction.CreateTransactionAsync(transaction);
        //    if (insertResult <= 0)
        //    {
        //        return Result<string>.SystemError(null, "Transaction process failed.");
        //    }

        //    return Result<string>.Success("Transaction completed successfully.");
        //}

        public async Task GetUserByPhoneNumberAsync(string phoneNumber)
        {
            var user =  await _db.TblWallets
                .Where(user => user.MobileNumber == phoneNumber)
                .FirstOrDefaultAsync();
            return;
        }

    }
}
