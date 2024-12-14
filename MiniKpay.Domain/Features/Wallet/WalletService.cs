using MiniKpay.Domain.Models.User;

namespace MiniKpay.Domain.Features.Wallet;

public class WalletService
{
    private readonly AppDbContext _db;

    public WalletService(AppDbContext db)
    {
        _db = db;
    }

    #region GetUserAsync

    public async Task <Result<List<UserResponseModel>> >GetUserAsync(int id)
    {
        Result<List<UserResponseModel>> model;

        try
        {
            var getUser = _db.TblWallets
                .Where(x => x.UserId == id)
                .AsNoTracking();

            if (getUser is null)
            {
                model = Result<List<UserResponseModel>>.ValidationError("No User Found");
                goto Result;
            }

            var lst = await getUser.Select(x => new UserResponseModel()
            {
             Wallet = x,
            }).ToListAsync();


            model = Result<List<UserResponseModel>>.Success(lst ,"Get User Successfully ");
            goto Result;

            Result:
            return model;
        }
        catch (Exception ex)
        {
            return Result<List<UserResponseModel>>.SystemError(ex.Message);
        }
    }

    #endregion

    #region CreateUserAsync

    public async Task<Result<UserResponseModel>> CreateUserAsync( TblWallet user )
    {
        try
        {
            Result<UserResponseModel> model = new Result<UserResponseModel> ();


            if (string.IsNullOrWhiteSpace(user.MobileNumber))
            {
                model = Result<UserResponseModel>.ValidationError("Mobile number cannot be empty.");
                goto Result;
            }
            if (string.IsNullOrWhiteSpace(user.PinCode))
            {
                model = Result<UserResponseModel>.ValidationError("PinCode cannot be empty");
                goto Result;
            }

          

            var response = new UserResponseModel 
            {
               Wallet = user
            };

           await  _db.TblWallets.AddAsync(user);
           await  _db.SaveChangesAsync();

            model = Result<UserResponseModel>.Success(response, "Create User Successfully");
            goto Result;

            Result:
            return model;
        }
        catch (Exception ex)
        {
            return Result<UserResponseModel>.SystemError(ex.Message);
        }
    }

    #endregion

    #region ChangePin

    public async Task<Result<UserResponseModel>> ChangePin(int id , string newPin )
    {
        try
        {
            Result<UserResponseModel> model = new Result<UserResponseModel>();

            var user = await _db.TblWallets.FirstOrDefaultAsync(u => u.UserId == id);

            if(user is null)
            {
                model = Result<UserResponseModel>.ValidationError("User are not found");
                goto Result;
            }

            if (string.IsNullOrWhiteSpace(newPin))
            {
                model = Result<UserResponseModel>.ValidationError("Pin code cannot be empty.");
                goto Result;
            }

            if (newPin.Length != 6)
            {
                model = Result<UserResponseModel>.ValidationError("Pin code must be exactly 6 characters.");
                goto Result;
            }

            user.PinCode = newPin;
            await _db.SaveChangesAsync();

            var responseModel = new UserResponseModel
            {
                Wallet = user,

            };
            Result:
            return model;
        }
        catch (Exception ex)
        {
            return Result<UserResponseModel>.SystemError(ex.Message);

        }
    }

    #endregion

}
