using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniKpay.Database.Models;
using MiniKpay.Domain.Models;
using MiniKpay.Domain.Models.User;

namespace MiniKpay.Domain.Features.Wallet
{
    public class WalletService
    {
        private readonly AppDbContext _db;

        public WalletService(AppDbContext db)
        {
            _db = db;
        }

        public async Task <Result<UserResponseModel>> GetUser(int id)
        {
             Result<UserResponseModel> model = new Result<UserResponseModel> ();

            var user = _db.TblWallets.AsNoTracking().FirstOrDefault(x => x.UserId == id);

            var responseModel = new UserResponseModel
            {
                WalletUser = user,
            };

           var result = Result<UserResponseModel>.Success(responseModel,"Get User Successfully");

            return result;
        }
    }
}
