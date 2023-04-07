namespace Patient_Health_Management_System.Repositories
{
    public interface IMedicineRepo
    {
        #region Medicine
        Task<List<Medicine>> GetMedicines();
        Task<List<Medicine>> GetMedicinesByPage(int page, int pageSize);
        Task<Medicine> GetMedicineById(string id);
        Task<List<Medicine>> GetMedicineByKeyword(string keyword);
        Task<List<Medicine>> GetMedicineByGroupName(string groupName);
        Task<Medicine> CreateMedicine(Medicine medicine);
        Task UpdateMedicineById(string id, Medicine medicine);
        Task DeleteMedicineById(string id, Medicine medicine);
        #endregion

        #region MedicineGroup
        Task<List<MedicineGroup>> GetMedicineGroups();
        Task<MedicineGroup> GetMedicineGroupById(string id);
        Task<List<MedicineGroup>> GetMedicineGroupByName(string name);
        Task<MedicineGroup> CreateMedicineGroup(MedicineGroup medicineGroup);
        Task DeleteMedicineGroupById(string id);
        Task UpdateMedicineGroupById(string id, MedicineGroup medicineGroup);
        #endregion
    }
}