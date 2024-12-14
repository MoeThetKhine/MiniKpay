using MiniKpay.Domain.Features.Wallet;

namespace MiniKpay.RestApi.Controllers.Wallet;

[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly WalletService _walletService;

    public WalletController(WalletService walletService)
    {
        _walletService = walletService;
    }

    #region GetUser

    [HttpGet]
    public async Task<IActionResult> GetUser(int id)
    {

        var user =await _walletService.GetUserAsync(id);
        return Ok(user);
    }

    #endregion

    #region CreateUser

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser(TblWallet reqUser )
    {
        var user = await _walletService.CreateUserAsync(reqUser);
        return Ok(user);
    }

    #endregion

    #region ChangePin

    [HttpPost("change-pin")]
    public async Task<IActionResult> ChangePin(int id,string newPin)
    {
        var user = await _walletService.ChangePin(id,newPin);
        return Ok(user);
    }

    #endregion

}
