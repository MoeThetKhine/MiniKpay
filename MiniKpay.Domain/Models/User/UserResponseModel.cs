using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniKpay.Database.Models;

namespace MiniKpay.Domain.Models.User
{
   public  class UserResponseModel
    {
        public TblWallet? WalletUser { get; set; }
    }
}
