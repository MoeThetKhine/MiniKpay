namespace MiniKpay.Database.Models;

public partial class TblDepositWithDraw
{
    public int DepositId { get; set; }

    public string MobileNumber { get; set; } = null!;

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public string TransactionType { get; set; } = null!;
}
