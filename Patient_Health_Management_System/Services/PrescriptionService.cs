namespace Patient_Health_Management_System.Services
{
    public class prescriptionRepo
    {
        private readonly PrescriptionRepo _presriptionRepo;

        public prescriptionRepo(PrescriptionRepo prescriptionRepo)
        {
            _presriptionRepo = prescriptionRepo;
        }

        public async Task<List<Prescription>> GetPrescriptions()
        {
            try
            {
                return await _presriptionRepo.GetPrescriptions();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Prescription>> GetPrescriptionsByPage(int page, int pageSize)
        {
            try
            {
                return await _presriptionRepo.GetPrescriptionsByPage(page, pageSize);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Prescription> GetPrescriptionById(string id)
        {
            try
            {
                return await _presriptionRepo.GetPrescriptionById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Prescription>> GetPrescriptionsByKeyword(string keyword)
        {
            try
            {
                return await _presriptionRepo.GetPrescriptionsByKeyword(keyword);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Prescription> CreatePrescription(PrescriptionForm prescriptionForm, string userId)
        {
            try
            {
                var prescription = new Prescription()
                {
                    PrescriptionId = GeneratePrescriptionId(),
                    PatientId = prescriptionForm.PatientId,
                    DoctorId = userId,
                    Note = prescriptionForm.Note,
                    MedicinesPrescription = prescriptionForm.MedicinesPrescription,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
                    UpdatedBy = "",
                    IsDeleted = false,
                    DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt),
                    DeletedBy = ""
                };
                return await _presriptionRepo.CreatePrescription(prescription);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdatePrescriptionById(string id, PrescriptionForm prescriptionForm, string userId)
        {
            try
            {
                var prescription = await _presriptionRepo.GetPrescriptionById(id);
                prescription.Note = prescriptionForm.Note;
                prescription.MedicinesPrescription = prescriptionForm.MedicinesPrescription;
                prescription.UpdatedAt = DateTime.Now;
                prescription.UpdatedBy = userId;
                await _presriptionRepo.ModifyPrescriptionById(id, prescription);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeletePrescriptionById(string id, string userId)
        {
            try
            {
                var prescription = await _presriptionRepo.GetPrescriptionById(id);
                prescription.IsDeleted = true;
                prescription.DeletedAt = DateTime.Now;
                prescription.DeletedBy = userId;
                await _presriptionRepo.ModifyPrescriptionById(id, prescription);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        } 
 
        private string GeneratePrescriptionId()
        {
            var randomNum = new Random();
            var prescriptionId = "DT" + randomNum.Next(10000000, 99999999).ToString();
            return prescriptionId;
        }
    }
}
