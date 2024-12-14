namespace MiniKpay.Domain.Models.User;

public class UserReqModel
{
    public string UserName { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string PinCode { get; set; } = null!;

    public decimal? Balance { get; set; }

}
