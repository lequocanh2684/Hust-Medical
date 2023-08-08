using Patient_Health_Management_System.Domain.Models;

namespace Patient_Health_Management_System.Repositories
{
    public class PrescriptionRepo : IPrescriptionRepo
    {
        private readonly IMongoCollection<Prescription> _prescriptions;

        public PrescriptionRepo(RepoInitialize mongoDbSetup)
        {
            _prescriptions = mongoDbSetup.GetDatabase().GetCollection<Prescription>("prescription");
        }

        public async Task<List<Prescription>> GetPrescriptions()
        {
            try
            {
                return await _prescriptions.Find(prescription => !prescription.IsDeleted).ToListAsync();
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

        public async Task<string> GetLastPrescriptionId()
        {
            try
            {
                var projection = Builders<Prescription>.Projection.Include(prescription => prescription.PrescriptionId);
                var lastPrescription = await _prescriptions.Find(prescription => true).Project(projection).SortByDescending(prescription => prescription.PrescriptionId).Limit(1).FirstOrDefaultAsync();
                return BsonSerializer.Deserialize<Prescription>(lastPrescription).PrescriptionId;
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

        public async Task ModifyPrescriptionById(Prescription prescription)
        {
            try
            {
                await _prescriptions.ReplaceOneAsync(p => p.Id == prescription.Id, prescription);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<MedicinePrescribed>> GetRevenueMedicinePrescribedByCreatedDay(DateTime date)
        {
            try
            {
                var filter = Builders<Prescription>.Filter;
                var filterDate = filter.Gte(prescription => prescription.CreatedAt, date.Date) & filter.Lt(prescription => prescription.CreatedAt, date.Date.AddDays(1));
                var listMedicinePrescribed = await _prescriptions.Find(filterDate).ToListAsync();
                return listMedicinePrescribed.SelectMany(prescription => prescription.MedicinePrescribed).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Prescription>> GetPrescriptionsByDoctorId(string doctorId)
        {
            try
            {
                var filter = Builders<Prescription>.Filter;
                var filterDoctorId = filter.Eq(p => p.CreatedBy, doctorId) & filter.Eq(p => p.IsDeleted, false);
                return await _prescriptions.Find(filterDoctorId).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeletePrescriptionById(List<Prescription> prescriptions)
        {
            var deletes = new List<WriteModel<Prescription>>();
            foreach (var prescription in prescriptions)
            {
                deletes.Add(new ReplaceOneModel<Prescription>(Builders<Prescription>.Filter.Eq(p => p.Id, prescription.Id), prescription));
            }
            await _prescriptions.BulkWriteAsync(deletes, new BulkWriteOptions() { IsOrdered = false });
        }

        public async Task<List<Prescription>> GetPrescriptionsByPatientId(string patientId)
        {
            try
            {
                var filter = Builders<Prescription>.Filter;
                var filterPatientId = filter.Eq(p => p.PatientId, patientId) & filter.Eq(p => p.IsDeleted, false);
                return await _prescriptions.Find(filterPatientId).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Prescription> GetPrescriptionsByMedicalExaminationId(string medicalExaminationId)
        {
            try
            {
                return await _prescriptions.Find(prescription => prescription.MedicalExaminationId == medicalExaminationId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
