namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IBillingService
    {
        Task<List<Billing>> GetBillings();
        Task<Billing> GetBillingById(string id);
        Task<Billing> CreateBilling(BillingForm billingForm, string userId);
        Task UpdateBillingById(string id, BillingForm billingForm, string userId);
        Task DeleteBillingById(string id, string userId);
    }
}
