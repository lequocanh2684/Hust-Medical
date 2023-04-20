namespace Patient_Health_Management_System.Services
{
    public class BillingService : IBillingService
    {
        private readonly IBillingRepo _billingRepo;

        public BillingService(IBillingRepo billingRepo)
        {
            _billingRepo = billingRepo;
        }

        public async Task<List<Billing>> GetBillings()
        {
            try
            {
                return await _billingRepo.GetBillings();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<Billing> GetBillingById(string id)
        {
            try
            {
                return await _billingRepo.GetBillingById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Billing> CreateBilling(BillingForm billingForm, string userId)
        {
            try
            {
                ValidateBillingForm(billingForm);
                var bill = new Billing()
                {
                    BillId = billingForm.BillId,
                    PatientId = billingForm.PatientId,
                    PrescriptionId = billingForm.PrescriptionId,
                    MedicinePrescribed = billingForm.MedicinePrescribed,
                    TotalPrice = billingForm.TotalPrice,
                    PaymentMethod = new string(""),
                    IsPaid = false,
                    PaidAt = DateTime.Parse(DefaultVariable.PaidAt),
                    PaidBy = null,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
                    UpdatedBy = null,
                    IsDeleted = false,
                    DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt),
                    DeletedBy = null
                };
                return await _billingRepo.CreateBilling(bill);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateBillingById(string id, BillingForm billingForm, string userId)
        {
            try
            {
                var bill = await _billingRepo.GetBillingById(id);
                if (bill == null)
                {
                    throw new Exception("Billing not found");
                }
                else
                {
                    bill.MedicinePrescribed = billingForm.MedicinePrescribed;
                    bill.TotalPrice = billingForm.TotalPrice;
                    bill.UpdatedAt = DateTime.Now;
                    bill.UpdatedBy = userId;
                    await _billingRepo.ModifyBillingById(bill);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteBillingById(string id, string userId)
        {
            try
            {
                var bill = await _billingRepo.GetBillingById(id);
                if (bill == null)
                {
                    throw new Exception("Billing not found");
                }
                else
                {
                    bill.IsDeleted = true;
                    bill.DeletedAt = DateTime.Now;
                    bill.DeletedBy = userId;
                    await _billingRepo.ModifyBillingById(bill);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ValidateBillingForm(BillingForm billingForm)
        {
            var regex = new Regex(@"^HD[0-9]{8}$");
            if (!regex.IsMatch(billingForm.BillId))
            {
                throw new Exception("BillId format is invalid");
            }
        }
    }
}
