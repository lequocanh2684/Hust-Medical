namespace Patient_Health_Management_System.Repositories
{
    public interface IDiseaseRepo
    {
        Task<List<Disease>> GetDiseases();
        Task<List<Disease>> GetDiseasesByPage(int page, int pageSize);
        Task<Disease> GetDiseaseById(string id);
        Task<Disease> GetDiseaseByDiseaseId(string diseaseId);
        Task<List<Disease>> GetDiseasesByName(string name);
        Task<Disease> CreateDisease(Disease disease);
        Task ModifyDiseaseById(string id, Disease disease);
    }
}
