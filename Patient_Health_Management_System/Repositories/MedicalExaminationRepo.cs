namespace Patient_Health_Management_System.Repositories
{
    public class MedicalExaminationRepo : IMedicalExaminationRepo
    {
        private readonly IMongoCollection<MedicalExamination> _medicalExaminations;
        public MedicalExaminationRepo(MongoDbSetup mongoDbSetup)
        {
            _medicalExaminations = mongoDbSetup.GetDatabase().GetCollection<MedicalExamination>("medical_examination");
        }
        public async Task<MedicalExamination> CreateMedicalExamination(MedicalExamination medicalExamination)
        {
            await _medicalExaminations.InsertOneAsync(medicalExamination);
            return medicalExamination;
        }
        public async Task<MedicalExamination> GetMedicalExaminationById(string id)
        {
            return await _medicalExaminations.Find(medicalExamination => medicalExamination.Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<MedicalExamination>> GetMedicalExaminationByKeyword(string keyword)
        {
            return await _medicalExaminations.Find(medicalExamination => medicalExamination.PatientId.Contains(keyword) || medicalExamination.Note.Contains(keyword)).ToListAsync();
        }
        public async Task<List<MedicalExamination>> GetMedicalExaminations()
        {
            return await _medicalExaminations.Find(medicalExamination => true).ToListAsync();
        }
        public async Task<List<MedicalExamination>> GetMedicalExaminationsByPage(int page, int pageSize)
        {
            return await _medicalExaminations.Find(medicalExamination => true).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();
        }
        public async Task ModifyMedicalExaminationById(MedicalExamination medicalExamination)
        {
            await _medicalExaminations.ReplaceOneAsync(me => me.Id == medicalExamination.Id, medicalExamination);
        }
    }
}
