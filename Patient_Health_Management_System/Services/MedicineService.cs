using System.Collections;

namespace Patient_Health_Management_System.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly MedicineRepo _medicineRepo;

        public MedicineService(MedicineRepo medicineRepo)
        {
            _medicineRepo = medicineRepo;
        }

        #region medicine
        public async Task<List<Medicine>> GetMedicines()
        {
            try
            {
                return await _medicineRepo.GetMedicines();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Medicine>> GetMedicinesByPage(int page, int pageSize)
        {
            try
            {
                return await _medicineRepo.GetMedicinesByPage(page, pageSize);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Medicine> GetMedicineById(string id)
        {
            try
            {
                return await _medicineRepo.GetMedicineById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Medicine>> GetMedicinesByName(string name)
        {
            try
            {
                return await _medicineRepo.GetMedicinesByName(name);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Medicine>> GetMedicineByKeyword(string keyword)
        {
            try
            {
                return await _medicineRepo.GetMedicineByKeyword(keyword);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Medicine>> GetMedicineByGroupName(string groupName)
        {
            try
            {
                return await _medicineRepo.GetMedicineByGroupName(groupName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Medicine> CreateMedicine(MedicineForm medicineForm, string userId)
        {
            try
            {
                var medicineIsExisted = await _medicineRepo.GetMedicinesByName(medicineForm.Name);
                var medicineIdIsExisted = await _medicineRepo.GetMedicineByMedicineId(medicineForm.MedicineId);
                if (medicineIsExisted.Any() || medicineIdIsExisted != null)
                {
                    throw new Exception("Medicine name already exists");
                }
                ValidateMedicineForm(medicineForm);
                var medicine = new Medicine
                {
                    MedicineId = medicineForm.MedicineId,
                    Name = medicineForm.Name,
                    GroupName = medicineForm.GroupName,
                    Unit = medicineForm.Unit,
                    HowToUse = medicineForm.HowToUse,
                    QuantityDefault = medicineForm.QuantityDefault,
                    ImportPrice = medicineForm.ImportPrice,
                    SellingPrice = medicineForm.SellingPrice,
                    MinimumStock = medicineForm.MinimumStock,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
                    UpdatedBy = null,
                    DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt),
                    DeletedBy = null
                };
                return await _medicineRepo.CreateMedicine(medicine);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateMedicineById(string id, MedicineForm medicineForm, string userId)
        {
            try
            {
                var medicine = await _medicineRepo.GetMedicineById(id);
                ValidateMedicineForm(medicineForm);
                medicine.Name = medicineForm.Name;
                medicine.GroupName = medicineForm.GroupName;
                medicine.Unit = medicineForm.Unit;
                medicine.HowToUse = medicineForm.HowToUse;
                medicine.QuantityDefault = medicineForm.QuantityDefault;
                medicine.ImportPrice = medicineForm.ImportPrice;
                medicine.SellingPrice = medicineForm.SellingPrice;
                medicine.MinimumStock = medicineForm.MinimumStock;
                medicine.UpdatedAt = DateTime.Now;
                medicine.UpdatedBy = userId;
                await _medicineRepo.ModifyMedicineById(id, medicine);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteMedicineById(string id, string userId)
        {
            try
            {
                var medicine = await _medicineRepo.GetMedicineById(id);
                medicine.IsDeleted = true;
                medicine.DeletedAt = DateTime.Now;
                medicine.DeletedBy = userId;
                await _medicineRepo.ModifyMedicineById(id, medicine);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ValidateMedicineForm(MedicineForm medicineForm)
        {
            if (String.IsNullOrEmpty(medicineForm.Name))
            {
                throw new Exception("Medicine name is null");
            }
            if (String.IsNullOrEmpty(medicineForm.GroupName))
            {
                throw new Exception("Medicine group name is null");
            }
            if (String.IsNullOrEmpty(medicineForm.MedicineId))
            {
                throw new Exception("Medicine id is null");
            }
        }
        #endregion

        #region medicine_group
        public async Task<List<MedicineGroup>> GetMedicineGroups()
        {
            try
            {
                return await _medicineRepo.GetMedicineGroups();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<MedicineGroup> CreateMedicineGroup(MedicineGroupForm medicineGroupForm, string userId)
        {
            try
            {
                var checkMedicineGroupIsExisted = await _medicineRepo.GetMedicineGroupByName(medicineGroupForm.Name);
                if (checkMedicineGroupIsExisted.Any())
                {
                    throw new Exception("Medicine group name already exists");
                }
                ValidateMedicineGroupForm(medicineGroupForm);
                var medicineGroup = new MedicineGroup
                {
                    Name = medicineGroupForm.Name,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
                    UpdatedBy = null,
                    DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt),
                    DeletedBy = null
                };
                return await _medicineRepo.CreateMedicineGroup(medicineGroup);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task DeleteMedicineGroupById(string id, string userId)
        {
            var medicineGroup = await _medicineRepo.GetMedicineGroupById(id);
            medicineGroup.IsDeleted = true;
            medicineGroup.DeletedAt = DateTime.Now;
            medicineGroup.DeletedBy = userId;
            await _medicineRepo.ModifyMedicineGroupById(id, medicineGroup);
        }

        public async Task UpdateMedicineGroupById(string id, MedicineGroupForm medicineGroupForm, string userId)
        {
            var medicineGroup = await _medicineRepo.GetMedicineGroupById(id);
            var medicines = await _medicineRepo.GetMedicineByGroupName(medicineGroup.Name);
            ValidateMedicineGroupForm(medicineGroupForm);
            medicineGroup.Name = medicineGroupForm.Name;
            medicineGroup.UpdatedAt = DateTime.Now;
            medicineGroup.UpdatedBy = userId;
            foreach (var medicine in medicines)
            {
                medicine.GroupName = medicineGroupForm.Name;
                await _medicineRepo.ModifyMedicineById(medicine.MedicineId, medicine);
            }
            await _medicineRepo.ModifyMedicineGroupById(id, medicineGroup);
        }

        private void ValidateMedicineGroupForm(MedicineGroupForm medicineGroupForm)
        {
            if (String.IsNullOrEmpty(medicineGroupForm.Name))
            {
                throw new Exception("Medicine group name is null");
            }
        }
        #endregion
    }
}
