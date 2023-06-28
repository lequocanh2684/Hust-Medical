
namespace Patient_Health_Management_System.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly IMongoCollection<Patient> _patient;

        public PatientRepo(RepoInitialize mongoDbSetup)
        {
            _patient = mongoDbSetup.GetDatabase().GetCollection<Patient>("patient");
        }

        public async Task CreatePatient(Patient patient)
        {
            try
            {
                await _patient.InsertOneAsync(patient);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Patient> GetPatientById(string id)
        {
            try
            {
                return await _patient.Find(p => p.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Patient>> GetPatients()
        {
            try
            {
                return await _patient.Find(p => !p.IsDeleted).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> GetLastPatientId()
        {
            try
            {
                var projection = Builders<Patient>.Projection.Include(patient => patient.PatientId);
                var lastPatient = await _patient.Find(_ => true).Project(projection).SortByDescending(patient => patient.PatientId).Limit(1).FirstOrDefaultAsync();
                return BsonSerializer.Deserialize<Patient>(lastPatient).PatientId;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task ModifyPatientById(Patient patient)
        {
            try
            {
                await _patient.ReplaceOneAsync(p => p.Id == patient.Id, patient);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
