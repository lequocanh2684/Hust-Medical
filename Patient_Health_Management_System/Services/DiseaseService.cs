namespace Patient_Health_Management_System.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseaseRepo _diseaseRepo;

        public DiseaseService(IDiseaseRepo diseaseRepo)
        {
            _diseaseRepo = diseaseRepo;
        }

        public async Task<List<Disease>> GetDiseases()
        {
            try
            {
                return await _diseaseRepo.GetDiseases();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Disease>> GetDiseasesByPage(int page, int pageSize)
        {
            try
            {
                return await _diseaseRepo.GetDiseasesByPage(page, pageSize);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Disease> GetDiseaseById(string id)
        {
            try
            {
                return await _diseaseRepo.GetDiseaseById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Disease>> GetDiseasesByName(string name)
        {
            try
            {
                return await _diseaseRepo.GetDiseasesByName(name);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Disease> CreateDisease(DiseaseForm diseaseForm, string userId)
        {
            try
            {
                var checkDiseaseByName = await _diseaseRepo.GetDiseasesByName(diseaseForm.Name);
                var checkDiseaseByDiseaseId = await _diseaseRepo.GetDiseaseByDiseaseId(diseaseForm.DiseaseId);
                if (checkDiseaseByName.Any() || checkDiseaseByDiseaseId != null)
                {
                    throw new Exception("Disease already exists");
                }
                ValidateDiseaseForm(diseaseForm);
                var disease = new Disease
                {
                    DiseaseId = diseaseForm.DiseaseId,
                    Name = diseaseForm.Name,
                    GroupName = diseaseForm.GroupName,
                    CreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
                    IsDeleted = false,
                    DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt),
                    DeletedBy = null
                };
                return await _diseaseRepo.CreateDisease(disease);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateDiseaseById(string id, DiseaseForm diseaseForm, string userId)
        {
            try
            {
                var disease = await _diseaseRepo.GetDiseaseById(id);
                if (disease == null)
                {
                    throw new Exception("Disease not found");
                }
                else
                {
                    ValidateDiseaseForm(diseaseForm);
                    disease.DiseaseId = diseaseForm.DiseaseId;
                    disease.Name = diseaseForm.Name;
                    disease.GroupName = diseaseForm.GroupName;
                    disease.UpdatedAt = DateTime.Now;
                    disease.UpdatedBy = userId;
                    await _diseaseRepo.ModifyDiseaseById(id, disease);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteDiseaseById(string id, string userId)
        {
            try
            {
                var disease = await _diseaseRepo.GetDiseaseById(id);
                if (disease == null)
                {
                    throw new Exception("Disease not found");
                }
                else
                {
                    disease.IsDeleted = true;
                    disease.DeletedAt = DateTime.Now;
                    disease.DeletedBy = userId;
                    await _diseaseRepo.ModifyDiseaseById(id, disease);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ValidateDiseaseForm(DiseaseForm diseaseForm)
        {
            var regex = new Regex(@"^[A-Z]\d{2}[.]\d{1}$");
            if (!regex.IsMatch(diseaseForm.DiseaseId))
            {
                throw new Exception("DiseaseId format is invalid");
            }
        }
    }
}
