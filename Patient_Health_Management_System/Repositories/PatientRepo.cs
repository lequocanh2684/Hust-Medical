namespace Patient_Health_Management_System.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        public Task<Patient> CreatePatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> GetPatientById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Patient>> GetPatients()
        {
            throw new NotImplementedException();
        }

        public Task<List<Patient>> GetPatientsByPage(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task ModifyPatientById(string id, Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
