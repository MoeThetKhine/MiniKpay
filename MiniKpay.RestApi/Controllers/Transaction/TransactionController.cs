using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniKpay.Domain.Features.Transaction;

namespace MiniKpay.RestApi.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _service;

        public TransactionController(TransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionAsync()
        {
            try
            {
                var lst = await _service.GetTransactionAsync();
                return Ok(lst);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, new
                {
                    error = ex.Message
                });
            }
        }

       
    }
}
