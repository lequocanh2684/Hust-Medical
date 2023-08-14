namespace Hust_Medical.Services.Interfaces
{
    public interface IBillingService
    {
        Task<List<Billing>> GetBillings();
        Task<Billing> GetBillingById(string id);
        Task<Billing> CreateBilling(BillingForm billingForm, string userId);
        Task UpdateBillingById(string id, BillingForm billingForm, string userId);
        Task DeleteBillingById(string id, string userId);
        Task UpdatePayingStatusById(string id, BillingForm billingForm, string userId);
        Task DeleteBillingsById(List<Billing> billings, string userId);
        Task<List<Billing>> GetBillingsByPatientId(string patientId);
        Task<Billing> GetBillingByPrescriptionId(string prescriptionId);
    }
}
