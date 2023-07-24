namespace Patient_Health_Management_System.Repositories
{
    public class MedicalExaminationRepo : IMedicalExaminationRepo
    {
        private readonly IMongoCollection<MedicalExamination> _medicalExaminations;
        public MedicalExaminationRepo(RepoInitialize mongoDbSetup)
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
            return await _medicalExaminations.Find(medicalExamination => !medicalExamination.IsDeleted).ToListAsync();
        }
        public async Task<List<MedicalExamination>> GetMedicalExaminationsByPage(int page, int pageSize)
        {
            return await _medicalExaminations.Find(medicalExamination => true).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();
        }
        public async Task<string> GetLastMedicalExaminationId()
        {
            var projection = Builders<MedicalExamination>.Projection.Include(medicalExamination => medicalExamination.MedicalExaminationId);
            var lastMedicalExamination = await _medicalExaminations.Find(medicalExamination => true).Project(projection).SortByDescending(medicalExamination => medicalExamination.MedicalExaminationId).Limit(1).FirstOrDefaultAsync();
            return BsonSerializer.Deserialize<MedicalExamination>(lastMedicalExamination).MedicalExaminationId;
        }
        public async Task ModifyMedicalExaminationById(MedicalExamination medicalExamination)
        {
            await _medicalExaminations.ReplaceOneAsync(me => me.Id == medicalExamination.Id, medicalExamination);
        }
        public async Task<List<MedicalExamination>> GetMedicalExaminationsByDoctorId(string doctorId)
        {
            try
            {
                var filter = Builders<MedicalExamination>.Filter;
                var filterDoctorId = filter.Eq(p => p.CreatedBy, doctorId) & filter.Eq(p => p.IsDeleted, false);
                return await _medicalExaminations.Find(filterDoctorId).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<long> GetNumberMedicalExaminations()
        {
            try
            {
                return await _medicalExaminations.Find(me => !me.IsDeleted).CountDocumentsAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteMedicalPrescriptionsById(List<MedicalExamination> medicalExaminations)
        {
            var deletes = new List<WriteModel<MedicalExamination>>();
            foreach (var medicalExamination in medicalExaminations)
            {
                deletes.Add(new ReplaceOneModel<MedicalExamination>(Builders<MedicalExamination>.Filter.Eq(me => me.Id, medicalExamination.Id), medicalExamination));
            }
            await _medicalExaminations.BulkWriteAsync(deletes, new BulkWriteOptions() { IsOrdered = false });
        }

        public async Task<List<MedicalExamination>> GetMedicalExaminationsByPatientId(string patientId)
        {
            try
            {
                var filter = Builders<MedicalExamination>.Filter;
                var filterPatientId = filter.Eq(me => me.PatientId, patientId) & filter.Eq(me => me.IsDeleted, false);
                return await _medicalExaminations.Find(filterPatientId).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
