namespace Patient_Health_Management_System.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalExaminationController : ControllerBase
    {
        private readonly IMedicalExaminationService _medicalExaminationService;

        public MedicalExaminationController(IMedicalExaminationService medicalExaminationService)
        {
            _medicalExaminationService = medicalExaminationService;
        }

        [HttpGet("/api/medical-examinations", Name = "GetMedicalExaminations")]
        public async Task<List<MedicalExamination>> GetMedicalExaminations()
        {
            return await _medicalExaminationService.GetMedicalExaminations();
        }

        [HttpGet("/api/medical-examinations/{id}", Name = "GetMedicalExaminationById")]
        public async Task<MedicalExamination?> GetMedicalExaminationById(string id)
        {
            return await _medicalExaminationService.GetMedicalExaminationById(id);
        }

        [HttpPost("/api/medical-examinations", Name = "CreateMedicalExamination")]
        public async Task<MedicalExamination> CreateMedicalExamination([FromForm] MedicalExaminationForm medicalExaminationForm, [FromQuery] string userId)
        {
            return await _medicalExaminationService.CreateMedicalExamination(medicalExaminationForm, userId);
        }

        [HttpPut("/api/medical-examinations/{id}", Name = "UpdateMedicalExaminationById")]
        public async Task UpdateMedicalExaminationById(string id, [FromForm] MedicalExaminationForm medicalExaminationForm, [FromQuery] string userId)
        {
            await _medicalExaminationService.UpdateMedicalExaminationById(id, medicalExaminationForm, userId);
        }

        [HttpDelete("/api/medical-examinations/{id}", Name = "DeleteMedicalExaminationById")]
        public async Task DeleteMedicalExaminationById(string id, [FromQuery] string userId)
        {
            await _medicalExaminationService.DeleteMedicalExaminationById(id, userId);
        }
    }
}