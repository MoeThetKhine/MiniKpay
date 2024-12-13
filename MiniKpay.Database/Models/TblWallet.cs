namespace MiniKpay.Database.Models;

#region TblWallet

public partial class TblWallet
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string PinCode { get; set; } = null!;

    public decimal? Balance { get; set; }
}

#endregion
