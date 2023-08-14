namespace Hust_Medical.Repositories.Interfaces
{
    public interface IDiseaseGroupRepo
    {
        Task<List<DiseaseGroup>> GetDiseaseGroups();
        Task<DiseaseGroup> GetDiseaseGroupById(string id);
        Task<List<DiseaseGroup>> GetDiseaseGroupByName(string name);
        Task<DiseaseGroup> CreateDiseaseGroup(DiseaseGroup DiseaseGroup);
        Task ModifyDiseaseGroupById(DiseaseGroup DiseaseGroup);
    }
}