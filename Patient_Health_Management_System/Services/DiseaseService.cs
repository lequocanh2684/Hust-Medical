using System.Collections;

namespace Patient_Health_Management_System.Services
{
    public class DiseaseService
    {
        private readonly DiseaseRepo _diseaseRepo;

        public DiseaseService(DiseaseRepo diseaseRepo)
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
                var checkDiseaseByDiseaseId = await _diseaseRepo.GetDiseaseById(diseaseForm.DiseaseId);
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
                ValidateDiseaseForm(diseaseForm);
                disease.DiseaseId = diseaseForm.DiseaseId;
                disease.Name = diseaseForm.Name;
                disease.GroupName = diseaseForm.GroupName;
                disease.UpdatedAt = DateTime.Now;
                disease.UpdatedBy = userId;
                await _diseaseRepo.ModifyDiseaseById(id, disease);
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
                disease.IsDeleted = true;
                disease.DeletedAt = DateTime.Now;
                disease.DeletedBy = userId;
                await _diseaseRepo.ModifyDiseaseById(id, disease);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ValidateDiseaseForm(DiseaseForm diseaseForm)
        {
            if (string.IsNullOrEmpty(diseaseForm.Name))
            {
                throw new Exception("Disease name is required");
            }
            if (string.IsNullOrEmpty(diseaseForm.DiseaseId))
            {
                throw new Exception("Disease id is required");
            }
            if (string.IsNullOrEmpty(diseaseForm.GroupName))
            {
                throw new Exception("Disease group name is required");
            }
        }
    }
}
