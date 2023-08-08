namespace Hust_Medical.Repositories.Interfaces
{
    public interface IMedicalExaminationRepo
    {
        Task<List<MedicalExamination>> GetMedicalExaminations();
        Task<List<MedicalExamination>> GetMedicalExaminationsByPage(int page, int pageSize);
        Task<MedicalExamination> GetMedicalExaminationById(string id);
        Task<List<MedicalExamination>> GetMedicalExaminationByKeyword(string keyword);
        Task<MedicalExamination> CreateMedicalExamination(MedicalExamination medicalExamination);
        Task<string> GetLastMedicalExaminationId();
        Task ModifyMedicalExaminationById(MedicalExamination medicalExamination);
        Task<List<MedicalExamination>> GetMedicalExaminationsByDoctorId(string doctorId);
        Task<long> GetNumberMedicalExaminations();
        Task DeleteMedicalPrescriptionsById(List<MedicalExamination> medicalExaminations);
        Task<List<MedicalExamination>> GetMedicalExaminationsByPatientId(string patientId);
    }
}
