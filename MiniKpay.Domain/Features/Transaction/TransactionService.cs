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

        public async Task<Result<List<TransactionModel>>> GetTransactionAsync()
        {
            Result<List<TransactionModel>> response;

            try
            {
                var transactions = _db.TblTransactions.AsNoTracking();

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
    }
}
