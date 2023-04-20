using Patient_Health_Management_System.Data;

namespace Patient_Health_Management_System.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly IMongoCollection<Patient> patients;

        public PatientRepo(MongoDbSetup mongoDbSetup)
        {
            patients = mongoDbSetup.GetDatabase().GetCollection<Patient>("patients");
        }
        public async Task CreatePatient(Patient patient)
        {
            try
            {
                await patients.InsertOneAsync(patient);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Patient?> GetPatientById(string id)
        {
            try
            {
                return await patients.Find(p => p.Id == id).FirstOrDefaultAsync();
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
                return await patients.Find(p => true).ToListAsync();
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
                await patients.ReplaceOneAsync(p => p.Id == patient.Id, patient);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
