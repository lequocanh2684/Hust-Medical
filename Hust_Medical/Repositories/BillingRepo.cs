namespace Hust_Medical.Repositories
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
            try
            {
                await _billing.InsertOneAsync(billing);
                return billing;
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
                return await _billing.Find(b => b.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Billing>> GetBillings()
        {
            try
            {
                return await _billing.Find(b => !b.IsDeleted).SortByDescending(b => b.Id).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task ModifyBillingById(Billing billing)
        {
            try
            {
                await _billing.ReplaceOneAsync(b => b.Id == billing.Id, billing);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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

        public async Task DeleteBillingsById(List<Billing> billings)
        {
            var deletes = new List<WriteModel<Billing>>();
            foreach (var billing in billings)
            {
                deletes.Add(new ReplaceOneModel<Billing>(Builders<Billing>.Filter.Eq(p => p.Id, billing.Id), billing));
            }
            await _billing.BulkWriteAsync(deletes, new BulkWriteOptions() { IsOrdered = false });
        }

        public async Task<List<Billing>> GetBillingsByPatientId(string patientId)
        {
            try
            {
                var filter = Builders<Billing>.Filter;
                var filterPatientId = filter.Eq(b => b.PatientId, patientId) & filter.Eq(p => p.IsDeleted, false);
                return await _billing.Find(filterPatientId).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Billing> GetBillingByPrescriptionId(string prescriptionId)
        {
            try
            {
                return await _billing.Find(b => b.PrescriptionId == prescriptionId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
