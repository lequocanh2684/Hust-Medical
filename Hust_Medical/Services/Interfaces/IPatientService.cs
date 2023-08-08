namespace Hust_Medical.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetPatients();
        Task<Patient> GetPatientById(string id);
        Task<Patient> CreatePatient(PatientForm patientForm, string userId);
        Task UpdatePatientById(string id, PatientForm patientForm, string userId);
        Task DeletePatientById(string id, string userId);
        Task<long> GetNumberPatientsByCreatedDay(DateTime date);
        Task<List<Patient>> GetPatientsByDoctorId(string doctorId);
        Task<long> GetNumberPatients();
    }
}