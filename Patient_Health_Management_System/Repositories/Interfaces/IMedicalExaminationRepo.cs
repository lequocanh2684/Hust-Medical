namespace Patient_Health_Management_System.Repositories.Interfaces
{
    public interface IMedicalExaminationRepo
    {
        Task<List<MedicalExamination>> GetMedicalExaminations();
        Task<List<MedicalExamination>> GetMedicalExaminationsByPage(int page, int pageSize);
        Task<MedicalExamination> GetMedicalExaminationById(string id);
        Task<List<MedicalExamination>> GetMedicalExaminationByKeyword(string keyword);
        Task<MedicalExamination> CreateMedicalExamination(MedicalExamination medicalExamination);
        Task ModifyMedicalExaminationById(MedicalExamination medicalExamination);
    }
}
