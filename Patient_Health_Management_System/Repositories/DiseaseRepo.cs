namespace Patient_Health_Management_System.Repositories
{
    public class DiseaseRepo
    {
        private readonly IMongoCollection<Disease> _diseases;
        public DiseaseRepo(MongoDbSetup mongoDbSetup)
        {
            _diseases = mongoDbSetup.GetDatabase().GetCollection<Disease>("diseases");
        }
        public async Task<List<Disease>> GetDiseases()
        {
            return await _diseases.Find(disease => true).ToListAsync();
        }
        public async Task<List<Disease>> GetDiseasesByPage(int page, int pageSize)
        {
            return await _diseases.Find(disease => true).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();
        }
        public async Task<Disease> GetDiseaseById(string id)
        {
            return await _diseases.Find(disease => disease.Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<Disease>> GetDiseasesByName(string name)
        {
            var filter = Builders<Disease>.Filter.Text(name, new TextSearchOptions { CaseSensitive = false });
            return await _diseases.Find(filter).ToListAsync();
        }
        public async Task<Disease> CreateDisease(Disease disease)
        {
            await _diseases.InsertOneAsync(disease);
            return disease;
        }
        public async Task ModifyDiseaseById(string id, Disease disease)
        {
            await _diseases.ReplaceOneAsync(disease => disease.Id == id, disease);
        }
    }
}
