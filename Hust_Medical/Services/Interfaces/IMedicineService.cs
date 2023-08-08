namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IMedicineService
    {
        #region medicine
        Task<List<Medicine>> GetMedicines();
        Task<List<Medicine>> GetMedicinesByPage(int page, int pageSize);
        Task<Medicine> GetMedicineById(string id);
        Task<List<Medicine>> GetMedicinesByName(string name);
        Task<List<Medicine>> GetMedicineByKeyword(string keyword);
        Task<List<Medicine>> GetMedicineByGroupName(string groupName);
        Task<Medicine> CreateMedicine(MedicineForm medicineForm, string userId);
        Task UpdateMedicineById(string id, MedicineForm medicineForm, string userId);
        Task DeleteMedicineById(string id, string userId);
        byte[] ExportToExcel();
        Task<List<Medicine>> ImportMedicineExcel(Workbook workbook, string userId);
        Task DeleteMultiMedicinesById(List<Medicine> medicines, string userId);
        #endregion

        #region medicine_group
        Task<List<MedicineGroup>> GetMedicineGroups();
        Task<MedicineGroup> CreateMedicineGroup(MedicineGroupForm medicineGroupForm, string userId);
        Task DeleteMedicineGroupById(string id, string userId);
        Task UpdateMedicineGroupById(string id, MedicineGroupForm medicineGroupForm, string userId);
        #endregion
    }
}