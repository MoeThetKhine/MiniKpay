using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniKpay.Database.Models;
using MiniKpay.Domain.Features.Wallet;
using MiniKpay.Domain.Models.DepositWithdraw;
using MiniKpay.Domain.Models.User;

namespace MiniKpay.RestApi.Controllers.DepositWithdraw
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositWithdrawController : ControllerBase
    {
        private readonly DepositWithdrawService _depositWithdrawService;

        public DepositWithdrawController(DepositWithdrawService depositWithdrawService)
        {
            _depositWithdrawService = depositWithdrawService;
        }

        [HttpPost ("deposit")]
        public async Task<IActionResult> Deposit(TblDepositWithDraw depositRequest, int Id)

        {

            var user = await _depositWithdrawService.Deposit(depositRequest,Id);
            return Ok(user);
        }

        [HttpPost ("withdraw")]
        public async Task<IActionResult> Withdraw(TblDepositWithDraw reqWithdraw, int Id)

        {

            var user = await _depositWithdrawService.Withdraw(reqWithdraw,Id);
            return Ok(user);
        }

    }
}
