namespace Patient_Health_Management_System.Repositories.Interfaces
{
    public interface IMedicineRepo
    {
        Task<List<Medicine>> GetMedicines();
        Task<List<Medicine>> GetMedicinesByPage(int page, int pageSize);
        Task<Medicine> GetMedicineById(string id);
        Task<Medicine> GetMedicineByMedicineId(string medicineId);
        Task<List<Medicine>> GetMedicineByKeyword(string keyword);
        Task<List<Medicine>> GetMedicinesByName(string name);
        Task<List<Medicine>> GetMedicineByGroupName(string groupName);
        Task<Medicine> CreateMedicine(Medicine medicine);
        Task ModifyMedicineById(Medicine medicine);
    }
}