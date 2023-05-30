namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IDiseaseService
    {
        #region Disease
        Task<List<Disease>> GetDiseases();
        Task<List<Disease>> GetDiseasesByPage(int page, int pageSize);
        Task<Disease> GetDiseaseById(string id);
        Task<List<Disease>> GetDiseasesByName(string name);
        Task<Disease> CreateDisease(DiseaseForm diseaseForm, string userId);
        Task UpdateDiseaseById(string id, DiseaseForm diseaseForm, string userId);
        Task DeleteDiseaseById(string id, string userId);
        #endregion

        #region Disease_group
        Task<List<DiseaseGroup>> GetDiseaseGroups();
        Task<DiseaseGroup> CreateDiseaseGroup(DiseaseGroupForm DiseaseGroupForm, string userId);
        Task DeleteDiseaseGroupById(string id, string userId);
        Task UpdateDiseaseGroupById(string id, DiseaseGroupForm DiseaseGroupForm, string userId);
        #endregion
    }
}