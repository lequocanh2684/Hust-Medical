namespace Patient_Health_Management_System.Services
{
    public class MedicalExaminationService : IMedicalExaminationService
    {
        private readonly IMedicalExaminationRepo _medicalExaminationRepo;
        public MedicalExaminationService(IMedicalExaminationRepo medicalExaminationRepo)
        {
            _medicalExaminationRepo = medicalExaminationRepo;
        }
        public async Task<List<MedicalExamination>> GetMedicalExaminations()
        {
            try
            {
                return await _medicalExaminationRepo.GetMedicalExaminations();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<MedicalExamination?> GetMedicalExaminationById(string id)
        {
            try
            {
                return await _medicalExaminationRepo.GetMedicalExaminationById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<MedicalExamination> CreateMedicalExamination(MedicalExaminationForm medicalExaminationForm, string userId)
        {
            try
            {
                ValidateMedicalExamForm(medicalExaminationForm);
                var medicalExamination = new MedicalExamination
                {
                    MedicalExaminationId = medicalExaminationForm.MedicalExaminationId,
                    PatientId = medicalExaminationForm.PatientId,
                    DiseaseId = medicalExaminationForm.DiseaseId,
                    Height = medicalExaminationForm.Height,
                    Weight = medicalExaminationForm.Weight,
                    BloodPressure = medicalExaminationForm.BloodPressure,
                    VascularIndex = medicalExaminationForm.VascularIndex,
                    BodyTemperature = medicalExaminationForm.BodyTemperature,
                    BreathingRate = medicalExaminationForm.BreathingRate,
                    Note = medicalExaminationForm.Note,
                    CreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
                    IsDeleted = false,
                    DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt),
                    DeletedBy = null
                };
                return await _medicalExaminationRepo.CreateMedicalExamination(medicalExamination);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task UpdateMedicalExaminationById(string id, MedicalExaminationForm medicalExaminationForm, string userId)
        {
            try
            {
                var medicalExamination = await _medicalExaminationRepo.GetMedicalExaminationById(id);
                if (medicalExamination == null)
                {
                    throw new Exception("MedicalExamination not found");
                }
                else
                {
                    ValidateMedicalExamForm(medicalExaminationForm);
                    medicalExamination.MedicalExaminationId = medicalExaminationForm.MedicalExaminationId;
                    medicalExamination.PatientId = medicalExaminationForm.PatientId;
                    medicalExamination.DiseaseId = medicalExaminationForm.DiseaseId;
                    medicalExamination.Height = medicalExaminationForm.Height;
                    medicalExamination.Weight = medicalExaminationForm.Weight;
                    medicalExamination.BloodPressure = medicalExaminationForm.BloodPressure;
                    medicalExamination.VascularIndex = medicalExaminationForm.VascularIndex;
                    medicalExamination.BodyTemperature = medicalExaminationForm.BodyTemperature;
                    medicalExamination.BreathingRate = medicalExaminationForm.BreathingRate;
                    medicalExamination.Note = medicalExaminationForm.Note;
                    medicalExamination.UpdatedBy = userId;
                    medicalExamination.UpdatedAt = DateTime.Now;
                    await _medicalExaminationRepo.ModifyMedicalExaminationById(medicalExamination);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task DeleteMedicalExaminationById(string id, string userId)
        {
            try
            {
                var medicalExamination = await _medicalExaminationRepo.GetMedicalExaminationById(id);
                if (medicalExamination == null)
                {
                    throw new Exception("MedicalExamination not found");
                }
                else
                {
                    medicalExamination.IsDeleted = true;
                    medicalExamination.DeletedBy = userId;
                    medicalExamination.DeletedAt = DateTime.Now;
                    await _medicalExaminationRepo.ModifyMedicalExaminationById(medicalExamination);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ValidateMedicalExamForm(MedicalExaminationForm medicalExaminationForm)
        {
            var regex = new Regex(@"^DK[0-9]{8}$");
            if (!regex.IsMatch(medicalExaminationForm.MedicalExaminationId))
            {
                throw new Exception("MedicalExaminationId format is invalid");
            }
        }
    }
}