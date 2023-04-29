namespace Patient_Health_Management_System.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepo _presriptionRepo;

        public PrescriptionService(IPrescriptionRepo prescriptionRepo)
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

        public async Task<Prescription> GetPrescriptionByPrescriptionId(string prescriptionId)
        {
            try
            {
                return await _presriptionRepo.GetPrescriptionByPrescriptionId(prescriptionId);
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
                ValidatePrescriptionForm(prescriptionForm);
                var checkPrescriptionExisted = await _presriptionRepo.GetPrescriptionByPrescriptionId(prescriptionForm.PrescriptionId);
                if (checkPrescriptionExisted != null)
                {
                    throw new Exception("Prescription already existes");
                }
                else
                {
                    var prescription = new Prescription()
                    {
                        PrescriptionId = prescriptionForm.PrescriptionId,
                        PatientId = prescriptionForm.PatientId,
                        Note = prescriptionForm.Note,
                        MedicineIds = prescriptionForm.MedicineIds,
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
                ValidatePrescriptionForm(prescriptionForm);
                var prescription = await _presriptionRepo.GetPrescriptionById(id);
                if (prescription == null)
                {
                    throw new Exception("Prescription not found");
                }
                else
                {
                    prescription.Note = prescriptionForm.Note;
                    prescription.MedicineIds = prescriptionForm.MedicineIds;
                    prescription.UpdatedAt = DateTime.Now;
                    prescription.UpdatedBy = userId;
                    await _presriptionRepo.ModifyPrescriptionById(prescription);
                }
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
                if (prescription == null)
                {
                    throw new Exception("Prescription not found");
                }
                else
                {
                    prescription.IsDeleted = true;
                    prescription.DeletedAt = DateTime.Now;
                    prescription.DeletedBy = userId;
                    await _presriptionRepo.ModifyPrescriptionById(prescription);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ValidatePrescriptionForm(PrescriptionForm prescriptionForm)
        {
            var regex = new Regex(@"^DT[0-9]{8}$");
            if (!regex.IsMatch(prescriptionForm.PrescriptionId))
            {
                throw new Exception("PrescriptionId format is invalid");
            }
        }
    }
}
