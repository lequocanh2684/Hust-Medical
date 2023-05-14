namespace Patient_Health_Management_System.Repositories
{
    public class BillingRepo : IBillingRepo
    {
        private readonly IMongoCollection<Billing> _billing;

        public BillingRepo(MongoDbSetup mongoDbSetup)
        {
            _billing = mongoDbSetup.GetDatabase().GetCollection<Billing>("billing");
        }

        public async Task<Billing> CreateBilling(Billing billing)
        {
            await _billing.InsertOneAsync(billing);
            return billing;
        }

        public async Task<Billing?> GetBillingById(string id)
        {
            return await _billing.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Billing>> GetBillings()
        {
            return await _billing.Find(b => !b.IsDeleted).ToListAsync();
        }

        public async Task ModifyBillingById(Billing billing)
        {
            await _billing.ReplaceOneAsync(b => b.Id == billing.Id, billing);
        }
    }
}
