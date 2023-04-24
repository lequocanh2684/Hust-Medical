namespace Patient_Health_Management_System.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _patientRepo;
        public PatientService(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        public async Task<List<Patient>> GetPatients()
        {
            try
            {
                return await _patientRepo.GetPatients();
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
                return await _patientRepo.GetPatientById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Patient> CreatePatient(PatientForm patientForm, string userId)
        {
            try
            {
                ValidatePatientForm(patientForm);
                var patient = new Patient()
                {
                    PatientId = patientForm.PatientId,
                    Name = patientForm.Name,
                    Age = GetAge(patientForm.DateOfBirth, DateTime.Today),
                    Ethnic = patientForm.Ethnic,
                    Address = patientForm.Address,
                    PhoneNumber = patientForm.PhoneNumber,
                    MedicalInsuranceNumber = patientForm.MedicalInsuranceNumber,
                    DateOfBirth = patientForm.DateOfBirth,
                    Gender = patientForm.Gender,
                    Email = patientForm.Email,
                    CreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
                    IsDeleted = false,
                    DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt),
                    DeletedBy = null
                };
                await _patientRepo.CreatePatient(patient);
                return patient;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdatePatientById(string id, PatientForm patientForm, string userId)
        {
            try
            {
                var patient = await _patientRepo.GetPatientById(id);
                if (patient == null)
                {
                    throw new Exception("Patient not found");
                }
                else
                {
                    ValidatePatientForm(patientForm);
                    patient.PatientId = patientForm.PatientId;
                    patient.Name = patientForm.Name;
                    patient.Age = GetAge(patientForm.DateOfBirth, DateTime.Today);
                    patient.Address = patientForm.Address;
                    patient.DateOfBirth = patientForm.DateOfBirth;
                    patient.Address = patientForm.Address;
                    patient.Email = patientForm.Email;
                    patient.Gender = patientForm.Gender;
                    patient.Ethnic = patientForm.Ethnic;
                    patient.MedicalInsuranceNumber = patientForm.MedicalInsuranceNumber;
                    patient.PhoneNumber = patientForm.PhoneNumber;
                    patient.UpdatedAt = DateTime.Now;
                    patient.UpdatedBy = userId;
                }
                await _patientRepo.ModifyPatientById(patient);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeletePatientById(string id, string userId)
        {
            try
            {
                var patient = await _patientRepo.GetPatientById(id);
                if (patient == null)
                {
                    throw new Exception("Patient not found");
                }
                else
                {
                    patient.IsDeleted = true;
                    patient.DeletedAt = DateTime.Now;
                    patient.DeletedBy = userId;
                }
                await _patientRepo.ModifyPatientById(patient);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ValidatePatientForm(PatientForm patientForm)
        {
            var regex = new Regex(@"^BN[0-9]{8}$");
            if (!regex.IsMatch(patientForm.PatientId))
            {
                throw new Exception("PatientId format is invalid");
            }
        }

        private int GetAge(DateTime? start, DateTime end)
        {
            return (end.Year - start.Value.Year - 1) + (((end.Month > start.Value.Month) || (end.Month == start.Value.Month) && (end.Day >= start.Value.Day)) ? 1 : 0);
        }
    }
}