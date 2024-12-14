namespace MiniKpay.Domain.Models.DepositWithdraw;

#region DepositWithdrawResModel

public class DepositWithdrawResModel
{
    public string MobileNumber { get; set; } = null!;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; } = null!;
}

#endregion
