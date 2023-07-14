namespace Patient_Health_Management_System.Repositories.Interfaces
{
    public interface IPatientRepo
    {
        Task<List<Patient>> GetPatients();
        Task<Patient> GetPatientById(string id);
        Task<string> GetLastPatientId();
        Task CreatePatient(Patient patient);
        Task ModifyPatientById(Patient patient);
        Task<long> GetNumberPatientsByCreatedDay(DateTime date);
    }
}
