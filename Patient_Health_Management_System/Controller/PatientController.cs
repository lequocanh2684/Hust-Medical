namespace Patient_Health_Management_System.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("/api/patients", Name = "GetPatients")]
        public async Task<List<Patient>> GetPatients()
        {
            return await _patientService.GetPatients();
        }

        [HttpGet("/api/patients/{id}", Name = "GetPatientById")]
        public async Task<Patient?> GetPatientById(string id)
        {
            return await _patientService.GetPatientById(id);
        }

        [HttpPost("api/patients", Name = "CreatePatient")]
        public async Task<Patient> CreatePatient([FromForm] PatientForm patientForm, [FromQuery] string userId)
        {
            return await _patientService.CreatePatient(patientForm, userId);
        }

        [HttpPut("/api/patients/{id}", Name = "UpdatePatientById")]
        public async Task UpdatePatientById(string id, [FromForm] PatientForm patientForm, [FromQuery] string userId)
        {
            await _patientService.UpdatePatientById(id, patientForm, userId);
        }

        [HttpDelete("/api/patients/{id}", Name = "DeletePatientById")]
        public async Task DeletePatientById(string id, [FromQuery] string userId)
        {
            await _patientService.DeletePatientById(id, userId);
        }
    }
}