using MiniKpay.Database.Models;
using MiniKpay.Domain.Models.DepositWithdraw;
using MiniKpay.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class DepositWithdrawService
{
    private readonly AppDbContext _db;

    public DepositWithdrawService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Result<DepositWithdrawResModel>> Deposit(TblDepositWithDraw depositRequest, int Id)
    {
        try
        {
            Result<DepositWithdrawResModel> model = new Result<DepositWithdrawResModel>();

            var withdraw = await _db.TblDepositWithdraws.FirstOrDefaultAsync(x => x.DepositId == Id);
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

     
            _db.TblDepositWithdraws.Add(depositRequest); 
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

    public async Task<Result<DepositWithdrawResModel>> Withdraw(TblDepositWithDraw reqWithdraw, int Id)
    {
        try
        {

            Result<DepositWithdrawResModel> model = new Result<DepositWithdrawResModel>();

            var withdraw= await _db.TblDepositWithdraws.FirstOrDefaultAsync(x => x.DepositId == Id);
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


            user.Balance -= withdraw.Amount;


            var transaction = new DepositWithdrawResModel
            {
                MobileNumber = user.MobileNumber,
                Amount = withdraw.Amount,
                TransactionType = "Withdraw",
            };

           
            _db.TblDepositWithdraws.Add(withdraw); 
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
}
