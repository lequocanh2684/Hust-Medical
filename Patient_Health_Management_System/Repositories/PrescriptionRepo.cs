namespace Patient_Health_Management_System.Repositories
{
    public class PrescriptionRepo : IPrescriptionRepo
    {
        private readonly IMongoCollection<Prescription> _prescriptions;

        public PrescriptionRepo(MongoDbSetup mongoDbSetup)
        {
            _prescriptions = mongoDbSetup.GetDatabase().GetCollection<Prescription>("prescription");
        }

        public async Task<List<Prescription>> GetPrescriptions()
        {
            try
            {
                return await _prescriptions.Find(prescription => true).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Prescription>> GetPrescriptionsByPage(int page, int pageSize)
        {
            try
            {
                return await _prescriptions.Find(prescription => true).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Prescription> GetPrescriptionById(string id)
        {
            try
            {
                return await _prescriptions.Find(prescription => prescription.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Prescription> GetPrescriptionByPrescriptionId(string prescriptionId)
        {
            try
            {
                return await _prescriptions.Find(prescription => prescription.PrescriptionId == prescriptionId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Prescription>> GetPrescriptionsByKeyword(string keyword)
        {
            try
            {
                var filter = Builders<Prescription>.Filter.Text(keyword, new TextSearchOptions { CaseSensitive = false });
                return await _prescriptions.Find(filter).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Prescription> CreatePrescription(Prescription prescription)
        {
            try
            {
                await _prescriptions.InsertOneAsync(prescription);
                return prescription;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task ModifyPrescriptionById(string id, Prescription prescription)
        {
            try
            {
                await _prescriptions.ReplaceOneAsync(p => p.Id == id, prescription);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
