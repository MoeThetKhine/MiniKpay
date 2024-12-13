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

        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> GetTransactionByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                var lst = await _service.GetTransactionByPhoneNumberAsync(phoneNumber);
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync(TransactionRequestModel request)
        {
            try
            {
                var item = await _service.CreateTransactionAsync(request);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message
                });
            }
        }

    }
}
