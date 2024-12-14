namespace MiniKpay.Domain.Features.Transaction;

public class DepositWithdrawService
{
    private readonly AppDbContext _db;

    public DepositWithdrawService(AppDbContext db)
    {
        _db = db;
    }

    #region Deposit

    public async Task<Result<DepositWithdrawResModel>> Deposit(TblDepositWithDraw depositRequest, int Id)
    {
        try
        {
            Result<DepositWithdrawResModel> model = new Result<DepositWithdrawResModel>();

            var withdraw = await _db.TblDepositWithDraws.FirstOrDefaultAsync(x => x.DepositId == Id);
            var user = await _db.TblWallets.FirstOrDefaultAsync(x => x.UserId == Id);

            if (user is null)
            {
                
               model =  Result<DepositWithdrawResModel>.SystemError("User not found.");
                goto Result;
            }

            user.Balance += depositRequest.Amount;

            var transaction = new DepositWithdrawResModel
            {
                MobileNumber = user.MobileNumber,
                Amount = depositRequest.Amount,
                TransactionType = "Deposit",
            };

     
            _db.TblDepositWithDraws.Add(depositRequest); 
            await _db.SaveChangesAsync();
           
            model = Result<DepositWithdrawResModel>.Success(transaction, "Deposit completed successfully.");
            goto Result;

        Result:
            return model;
        }
        catch (Exception ex)
        {
            return Result<DepositWithdrawResModel>.SystemError($"Deposit failed: {ex.Message}");
        }
    }

    #endregion

    #region Withdraw

    public async Task<Result<DepositWithdrawResModel>> Withdraw(TblDepositWithDraw reqWithdraw, int Id)
    {
        try
        {

            Result<DepositWithdrawResModel> model = new Result<DepositWithdrawResModel>();

            var withdraw= await _db.TblDepositWithDraws.FirstOrDefaultAsync(x => x.DepositId == Id);
            var user = await _db.TblWallets.FirstOrDefaultAsync(x => x.UserId == Id);

            if (user is null)
            {
                return Result<DepositWithdrawResModel>.SystemError("User not found.");
            }

            if (user.Balance < withdraw.Amount)
            {
               model = Result<DepositWithdrawResModel>.SystemError("Insufficient balance.");
                goto Result;
            }


            user.Balance -= reqWithdraw.Amount;


            var transaction = new DepositWithdrawResModel
            {
                MobileNumber = user.MobileNumber,
                Amount = withdraw.Amount,
                TransactionType = "Withdraw",
            };

           
            _db.TblDepositWithDraws.Add(reqWithdraw); 
            await _db.SaveChangesAsync();

             model = Result<DepositWithdrawResModel>.Success(transaction, "Withdrawal completed successfully.");
            goto Result;

        Result:
            return model;
        }
        catch (Exception ex)
        {
            return Result<DepositWithdrawResModel>.SystemError($"Withdrawal failed: {ex.Message}");
        }
    }
    #endregion
}
