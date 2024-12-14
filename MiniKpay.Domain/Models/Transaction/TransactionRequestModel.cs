namespace MiniKpay.Domain.Models.Transaction;

#region TransactionRequestModel

public class TransactionRequestModel
{
    public string SenderMobileNo { get; set; } = null!;

    public string ReceiverMobileNo { get; set; } = null!;

    public decimal? Amount { get; set; }

    public string? Notes { get; set; }
}

#endregion
