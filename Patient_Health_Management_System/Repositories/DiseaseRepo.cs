using Patient_Health_Management_System.Domain.Models;

namespace Patient_Health_Management_System.Repositories
{
    public class DiseaseRepo : IDiseaseRepo, IDiseaseGroupRepo
    {
        private readonly IMongoCollection<Disease> _diseases;
        private readonly IMongoCollection<DiseaseGroup> _diseaseGroups;

        public DiseaseRepo(RepoInitialize mongoDbSetup)
        {
            _diseases = mongoDbSetup.GetDatabase().GetCollection<Disease>("diseases");
            _diseaseGroups = mongoDbSetup.GetDatabase().GetCollection<DiseaseGroup>("diseases_group");
        }

        #region Disease
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
        public async Task<Disease> GetDiseaseByDiseaseId(string diseaseId)
        {
            return await _diseases.Find(disease => disease.DiseaseId == diseaseId).FirstOrDefaultAsync();
        }
        public async Task<List<Disease>> GetDiseasesByName(string name)
        {
            var filter = Builders<Disease>.Filter.Text(name, new TextSearchOptions { CaseSensitive = false });
            return await _diseases.Find(filter).ToListAsync();
        }
        public async Task<List<Disease>> GetDiseasesByGroupName(string groupName)
        {
            return await _diseases.Find(disease => disease.GroupName == groupName).ToListAsync();
        }
        public async Task<Disease> CreateDisease(Disease disease)
        {
            await _diseases.InsertOneAsync(disease);
            return disease;
        }
        public async Task ModifyDiseaseById(Disease disease)
        {
            await _diseases.ReplaceOneAsync(d => d.Id == disease.Id, disease);
        }
        #endregion

        #region DiseaseGroup
        public async Task<List<DiseaseGroup>> GetDiseaseGroups()
        {
            return await _diseaseGroups.Find(DiseaseGroup => !DiseaseGroup.IsDeleted).ToListAsync();
        }

        public async Task<DiseaseGroup> GetDiseaseGroupById(string id)
        {
            return await _diseaseGroups.Find(DiseaseGroup => DiseaseGroup.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<DiseaseGroup>> GetDiseaseGroupByName(string name)
        {
            var filter = Builders<DiseaseGroup>.Filter.And(Builders<DiseaseGroup>.Filter.Text(name, new TextSearchOptions { CaseSensitive = false }), Builders<DiseaseGroup>.Filter.Eq(DiseaseGroup => DiseaseGroup.IsDeleted, false));
            return await _diseaseGroups.Find(filter).ToListAsync();
        }

        public async Task<DiseaseGroup> CreateDiseaseGroup(DiseaseGroup DiseaseGroup)
        {
            await _diseaseGroups.InsertOneAsync(DiseaseGroup);
            return DiseaseGroup;
        }

        public async Task ModifyDiseaseGroupById(DiseaseGroup DiseaseGroup)
        {
            await _diseaseGroups.ReplaceOneAsync(m => m.Id == DiseaseGroup.Id, DiseaseGroup);
        }
        #endregion
    }
}
