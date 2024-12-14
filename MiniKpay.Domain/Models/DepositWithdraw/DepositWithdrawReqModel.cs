using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniKpay.Domain.Models.DepositWithdraw
{
    public class TblDepositWithdraw
    {

        public string MobileNumber { get; set; } = null!;

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; } = null!;
    }
}
