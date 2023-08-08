namespace Patient_Health_Management_System.Services
{
    public interface IMedicalExaminationService
    {
        Task<List<MedicalExamination>> GetMedicalExaminations();
        Task<MedicalExamination?> GetMedicalExaminationById(string id);
        Task<MedicalExamination> CreateMedicalExamination(MedicalExaminationForm medicalExaminationForm, string userId);
        Task UpdateMedicalExaminationById(string id, MedicalExaminationForm medicalExaminationForm, string userId);
        Task DeleteMedicalExaminationById(string id, string userId);
        Task<List<MedicalExamination>> GetMedicalExaminationsByDoctorId(string doctorId);
        Task<long> GetNumberMedicalExaminations();
        Task DeleteMedicalExaminationsById(List<MedicalExamination> medicalExaminations, string userId);
        Task<List<MedicalExamination>> GetMedicalExaminationsByPatientId(string patientId);
    }
}