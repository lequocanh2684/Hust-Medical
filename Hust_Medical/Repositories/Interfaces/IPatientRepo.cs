namespace Hust_Medical.Repositories.Interfaces
{
    public interface IPatientRepo
    {
        Task<List<Patient>> GetPatients();
        Task<Patient> GetPatientById(string id);
        Task<string> GetLastPatientId();
        Task CreatePatient(Patient patient);
        Task ModifyPatientById(Patient patient);
        Task<long> GetNumberPatientsByCreatedDay(DateTime date);
        Task<List<Patient>> GetPatientsByDoctorId(string doctorId);
        Task<long> GetNumberPatients();
    }
}
