namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IDiseaseService
    {
        Task<List<Disease>> GetDiseases();
        Task<List<Disease>> GetDiseasesByPage(int page, int pageSize);
        Task<Disease> GetDiseaseById(string id);
        Task<List<Disease>> GetDiseasesByName(string name);
        Task<Disease> CreateDisease(DiseaseForm diseaseForm, string userId);
        Task UpdateDiseaseById(string id, DiseaseForm diseaseForm, string userId);
        Task DeleteDiseaseById(string id, string userId);
    }
}