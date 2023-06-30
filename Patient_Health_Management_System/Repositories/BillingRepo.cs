using Patient_Health_Management_System.Domain.Models;

namespace Patient_Health_Management_System.Repositories
{
    public class BillingRepo : IBillingRepo
    {
        private readonly IMongoCollection<Billing> _billing;

        public BillingRepo(RepoInitialize repoInitialize)
        {
            _billing = repoInitialize.GetDatabase().GetCollection<Billing>("billing");
        }

        public async Task<Billing> CreateBilling(Billing billing)
        {
            await _billing.InsertOneAsync(billing);
            return billing;
        }

        public async Task<Billing> GetBillingById(string id)
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

        public async Task<string> GetLastBillingId()
        {
            try
            {
                var projection = Builders<Billing>.Projection.Include(billing => billing.BillId);
                var lastBilling = await _billing.Find(_ => true).Project(projection).SortByDescending(billing => billing.BillId).Limit(1).FirstOrDefaultAsync();
                return BsonSerializer.Deserialize<Billing>(lastBilling).BillId;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
