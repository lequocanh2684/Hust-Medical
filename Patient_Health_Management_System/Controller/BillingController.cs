namespace Patient_Health_Management_System.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpGet("/api/billings", Name = "GetBillings")]
        public async Task<List<Billing>> GetBillings()
        {
            return await _billingService.GetBillings();
        }

        [HttpGet("/api/billings/{id}", Name = "GetBillingById")]
        public async Task<Billing?> GetBillingById(string id)
        {
            return await _billingService.GetBillingById(id);
        }

        [HttpPost("/api/billings", Name = "CreateBilling")]
        public async Task<Billing> CreateBilling([FromForm] BillingForm billingForm, [FromQuery] string userId)
        {
            return await _billingService.CreateBilling(billingForm, userId);
        }

    }
}