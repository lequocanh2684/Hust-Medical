namespace Patient_Health_Management_System.Repositories
{
    public interface IPrescriptionRepo
    {
        Task<List<Prescription>> GetPrescriptions();
        Task<List<Prescription>> GetPrescriptionsByPage(int page, int pageSize);
        Task<Prescription> GetPrescriptionById(string id);
        Task<Prescription> GetPrescriptionByPrescriptionId(string prescriptionId);
        Task<List<Prescription>> GetPrescriptionsByKeyword(string keyword);
        Task<Prescription> CreatePrescription(Prescription prescription);
        Task ModifyPrescriptionById(string id, Prescription prescription);

    }
}
