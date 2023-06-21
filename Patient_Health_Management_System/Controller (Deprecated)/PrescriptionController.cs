namespace Patient_Health_Management_System.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet("/api/prescriptions", Name = "GetPrescriptions")]
        public async Task<List<Prescription>> GetPrescriptions()
        {
            return await _prescriptionService.GetPrescriptions();
        }

        [HttpGet("/api/prescriptions/{id}", Name = "GetPrescriptionById")]
        public async Task<Prescription?> GetPrescriptionById(string id)
        {
            return await _prescriptionService.GetPrescriptionById(id);
        }

        [HttpPost("/api/prescriptions", Name = "CreatePrescription")]
        public async Task<Prescription> CreatePrescription([FromForm] PrescriptionForm prescriptionForm, [FromQuery] string userId)
        {
            return await _prescriptionService.CreatePrescription(prescriptionForm, userId);
        }

        [HttpPut("/api/prescriptions/{id}", Name = "UpdatePrescriptionById")]
        public async Task UpdatePrescriptionById(string id, [FromForm] PrescriptionForm prescriptionForm, [FromQuery] string userId)
        {
            await _prescriptionService.UpdatePrescriptionById(id, prescriptionForm, userId);
        }

        [HttpDelete("/api/prescriptions/{id}", Name = "DeletePrescriptionById")]
        public async Task DeletePrescriptionById(string id, [FromQuery] string userId)
        {
            await _prescriptionService.DeletePrescriptionById(id, userId);
        }
    }
}