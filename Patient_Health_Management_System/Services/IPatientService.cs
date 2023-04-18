namespace Patient_Health_Management_System.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> GetPatients();
        Task<Patient?> GetPatientById(Guid id);
        Task<Patient> CreatePatient(PatientForm patientForm, string userId);
        Task UpdatePatientById(Guid id, PatientForm patientForm, string userId);
        Task DeletePatientById(Guid id, string userId);
    }
}