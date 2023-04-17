namespace Patient_Health_Management_System.Repositories
{
    public interface IPatientRepo
    {
        Task<List<Patient>> GetPatients();
        Task<List<Patient>> GetPatientsByPage(int page, int pageSize);
        Task<Patient> GetPatientById(string id);
        Task<Patient> CreatePatient(Patient patient);
        Task ModifyPatientById(string id, Patient patient);
    }
}
