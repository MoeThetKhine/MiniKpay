using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniKpay.Database.Models;
using MiniKpay.Domain.Features.Wallet;
using MiniKpay.Domain.Models.User;

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

            var user =await _walletService.GetUserAsync(id);
            return Ok(user);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(TblWallet reqUser )

        {

            var user = await _walletService.CreateUserAsync(reqUser);
            return Ok(user);
        }

        [HttpPost("change-pin")]
        public async Task<IActionResult> ChangePin(int id,string newPin)

        {

            var user = await _walletService.ChangePin(id,newPin);
            return Ok(user);
        }
    }
}
