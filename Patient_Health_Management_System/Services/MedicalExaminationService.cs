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
                var medicalExamination = new MedicalExamination
                {
                    MedicalExaminationId = await AutoGenerateNewMedicalExaminationId(),
                    PatientId = medicalExaminationForm.PatientId,
                    DiseaseName = medicalExaminationForm.DiseaseName,
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
                    medicalExamination.MedicalExaminationId = await AutoGenerateNewMedicalExaminationId();
                    medicalExamination.PatientId = medicalExaminationForm.PatientId;
                    medicalExamination.DiseaseName = medicalExaminationForm.DiseaseName;
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

        public async Task<List<MedicalExamination>> GetMedicalExaminationsByDoctorId(string doctorId)
        {
            try
            {
                return await _medicalExaminationRepo.GetMedicalExaminationsByDoctorId(doctorId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<long> GetNumberMedicalExaminations()
        {
            try
            {
                return await _medicalExaminationRepo.GetNumberMedicalExaminations();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /*Deprecated*/
        //private void ValidateMedicalExamForm(MedicalExaminationForm medicalExaminationForm)
        //{
        //    var regex = new Regex(@"^DK[0-9]{8}$");
        //    if (!regex.IsMatch(medicalExaminationForm.MedicalExaminationId))
        //    {
        //        throw new Exception("MedicalExaminationId format is invalid");
        //    }
        //}

        private async Task<string> AutoGenerateNewMedicalExaminationId()
        {
            try
            {
                var lastMedicalExaminationId = await _medicalExaminationRepo.GetLastMedicalExaminationId();
                var newMedicalExaminationId = int.Parse(lastMedicalExaminationId.Substring(2, 8)) + 1;
                return "DK" + newMedicalExaminationId.ToString("D8");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}