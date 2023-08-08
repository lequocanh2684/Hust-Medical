namespace Hust_Medical.Services
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
                var prescription = new Prescription()
                {
                    PrescriptionId = await AutoGenerateNewPrescriptionId(),
                    PatientId = prescriptionForm.PatientId,
                    Note = prescriptionForm.Note,
                    MedicinePrescribed = prescriptionForm.MedicinePrescribed,
                    MedicalExaminationId = prescriptionForm.MedicalExaminationId,
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
                if (prescription == null)
                {
                    throw new Exception("Prescription not found");
                }
                else
                {
                    prescription.Note = prescriptionForm.Note;
                    prescription.MedicinePrescribed = prescriptionForm.MedicinePrescribed;
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

        public async Task<int> GetRevenueMedicinePrescribedByCreatedDay(DateTime date)
        {
            try
            {
                var listMedicinePrescribed = await _presriptionRepo.GetRevenueMedicinePrescribedByCreatedDay(date);
                return listMedicinePrescribed.Sum(medicinePrescribed => medicinePrescribed.BuyingQuantity * medicinePrescribed.SellingPrice);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Prescription>> GetPrescriptionsByDoctorId(string doctorId)
        {
            try
            {
                return await _presriptionRepo.GetPrescriptionsByDoctorId(doctorId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeletePrescriptionById(List<Prescription> prescriptions, string userId)
        {
            try
            {
                for (int i = 0; i < prescriptions.Count; i++)
                {
                    prescriptions[i].IsDeleted = true;
                    prescriptions[i].DeletedAt = DateTime.Now;
                    prescriptions[i].DeletedBy = userId;
                }
                await _presriptionRepo.DeletePrescriptionById(prescriptions);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Prescription>> GetPrescriptionsByPatientId(string patientId)
        {
            try
            {
                return await _presriptionRepo.GetPrescriptionsByPatientId(patientId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Prescription> GetPrescriptionsByMedicalExaminationId(string medicalExaminationId)
        {
            try
            {
                return await _presriptionRepo.GetPrescriptionsByMedicalExaminationId(medicalExaminationId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private async Task<string> AutoGenerateNewPrescriptionId()
        {
            try
            {
                var lastPrescriptionId = await _presriptionRepo.GetLastPrescriptionId();
                var newPrescriptionId = int.Parse(lastPrescriptionId.Substring(2, 8)) + 1;
                return "DT" + newPrescriptionId.ToString("D8");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /*Deprecated*/
        //private void ValidatePrescriptionForm(PrescriptionForm prescriptionForm)
        //{
        //    var regex = new Regex(@"^DT[0-9]{8}$");
        //    if (!regex.IsMatch(prescriptionForm.PrescriptionId))
        //    {
        //        throw new Exception("PrescriptionId format is invalid");
        //    }
        //}
    }
}
