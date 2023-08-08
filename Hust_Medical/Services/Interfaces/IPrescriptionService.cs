namespace Hust_Medical.Services.Interfaces
{
    public interface IPrescriptionService
    {
        Task<List<Prescription>> GetPrescriptions();
        Task<List<Prescription>> GetPrescriptionsByPage(int page, int pageSize);
        Task<Prescription> GetPrescriptionById(string id);
        Task<Prescription> GetPrescriptionByPrescriptionId(string prescriptionId);
        Task<List<Prescription>> GetPrescriptionsByKeyword(string keyword);
        Task<Prescription> CreatePrescription(PrescriptionForm prescriptionForm, string userId);
        Task UpdatePrescriptionById(string id, PrescriptionForm prescriptionForm, string userId);
        Task DeletePrescriptionById(string id, string userId);
        Task<int> GetRevenueMedicinePrescribedByCreatedDay(DateTime date);
        Task<List<Prescription>> GetPrescriptionsByDoctorId(string doctorId);
        Task DeletePrescriptionById(List<Prescription> prescriptions, string userId);
        Task<List<Prescription>> GetPrescriptionsByPatientId(string patientId);
        Task<Prescription> GetPrescriptionsByMedicalExaminationId(string medicalExaminationId);
    }
}
