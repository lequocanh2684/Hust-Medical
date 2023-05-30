using Patient_Health_Management_System.Repositories.Interfaces;

namespace Patient_Health_Management_System.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseaseRepo _diseaseRepo;
        private readonly IDiseaseGroupRepo _diseaseGroupRepo;

        public DiseaseService(IDiseaseRepo diseaseRepo, IDiseaseGroupRepo diseaseGroupRepo)
        {
            _diseaseRepo = diseaseRepo;
            _diseaseGroupRepo = diseaseGroupRepo;
        }

        #region Disease
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
                    await _diseaseRepo.ModifyDiseaseById(disease);
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
                    await _diseaseRepo.ModifyDiseaseById(disease);
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
        #endregion

        #region Disease_group
        public async Task<List<DiseaseGroup>> GetDiseaseGroups()
        {
            try
            {
                return await _diseaseGroupRepo.GetDiseaseGroups();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<DiseaseGroup> CreateDiseaseGroup(DiseaseGroupForm diseaseGroupForm, string userId)
        {
            try
            {
                var checkDiseaseGroupIsExisted = await _diseaseGroupRepo.GetDiseaseGroupByName(diseaseGroupForm.Name);
                if (checkDiseaseGroupIsExisted.Any())
                {
                    throw new Exception("Disease group name already exists");
                }
                var diseaseGroup = new DiseaseGroup
                {
                    Name = diseaseGroupForm.Name,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
                    UpdatedBy = null,
                    DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt),
                    DeletedBy = null
                };
                return await _diseaseGroupRepo.CreateDiseaseGroup(diseaseGroup);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task DeleteDiseaseGroupById(string id, string userId)
        {
            var DiseaseGroup = await _diseaseGroupRepo.GetDiseaseGroupById(id);
            if (DiseaseGroup == null)
            {
                throw new Exception("Disease group not found");
            }
            else
            {
                DiseaseGroup.IsDeleted = true;
                DiseaseGroup.DeletedAt = DateTime.Now;
                DiseaseGroup.DeletedBy = userId;
                await _diseaseGroupRepo.ModifyDiseaseGroupById(DiseaseGroup);
            }
        }

        public async Task UpdateDiseaseGroupById(string id, DiseaseGroupForm DiseaseGroupForm, string userId)
        {
            var DiseaseGroup = await _diseaseGroupRepo.GetDiseaseGroupById(id);
            var Diseases = await _diseaseRepo.GetDiseasesByGroupName(DiseaseGroup.Name);
            if (DiseaseGroup == null)
            {
                throw new Exception("Disease group not found");
            }
            else if (!Diseases.Any())
            {
                throw new Exception("List of Diseases not found");
            }
            else
            {
                DiseaseGroup.Name = DiseaseGroupForm.Name;
                DiseaseGroup.UpdatedAt = DateTime.Now;
                DiseaseGroup.UpdatedBy = userId;
                foreach (var Disease in Diseases)
                {
                    Disease.GroupName = DiseaseGroupForm.Name;
                    await _diseaseRepo.ModifyDiseaseById(Disease);
                }
                await _diseaseGroupRepo.ModifyDiseaseGroupById(DiseaseGroup);
            }
        }
        #endregion
    }
}
