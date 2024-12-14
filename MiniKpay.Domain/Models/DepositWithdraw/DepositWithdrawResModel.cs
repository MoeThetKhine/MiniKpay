using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniKpay.Database.Models;

namespace MiniKpay.Domain.Models.DepositWithdraw
{
    public class DepositWithdrawResModel
    {
     
        public string MobileNumber { get; set; } = null!;

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; } = null!;
    }
}
