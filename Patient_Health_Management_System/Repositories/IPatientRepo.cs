namespace Patient_Health_Management_System.Repositories
{
    public interface IPatientRepo
    {
        Task<List<Patient>> GetPatients();
        Task<Patient?> GetPatientById(Guid id);
        Task CreatePatient(Patient patient);
        Task ModifyPatientById(Patient patient);
    }
}
