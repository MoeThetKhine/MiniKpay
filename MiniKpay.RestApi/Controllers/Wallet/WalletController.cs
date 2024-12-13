using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniKpay.Domain.Features.Wallet;

namespace MiniKpay.RestApi.Controllers.Wallet
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly WalletService _walletService;

        public WalletController(WalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {

            var user = _walletService.GetUserAsync(id);
            return Ok(user);
        }
    }
}
