namespace Hust_Medical.Repositories.Interfaces
{
    public interface IBillingRepo
    {
        Task<List<Billing>> GetBillings();
        Task<Billing> GetBillingById(string id);
        Task<Billing> CreateBilling(Billing billing);
        Task ModifyBillingById(Billing billing);
        Task<string> GetLastBillingId();
        Task DeleteBillingsById(List<Billing> billings);
        Task<List<Billing>> GetBillingsByPatientId(string patientId);
        Task<Billing> GetBillingByPrescriptionId(string prescriptionId);
    }
}
