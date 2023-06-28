namespace Patient_Health_Management_System.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepo _medicineRepo;
        private readonly IMedicineGroupRepo _medicineGroupRepo;

        public MedicineService(IMedicineRepo medicineRepo, IMedicineGroupRepo medicineGroupRepo)
        {
            _medicineRepo = medicineRepo;
            _medicineGroupRepo = medicineGroupRepo;
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
                if (medicineIsExisted.Any())
                {
                    throw new Exception("Medicine name already exists");
                }
                //ValidateMedicineForm(medicineForm);
                var medicine = new Medicine
                {
                    MedicineId = await AutoGenerateNewMedicineId(),
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
                if (medicine == null)
                {
                    throw new Exception("Medicine not found");
                }
                else
                {
                    //ValidateMedicineForm(medicineForm);
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
                    await _medicineRepo.ModifyMedicineById(medicine);
                }
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
                if (medicine == null)
                {
                    throw new Exception("Medicine not found");
                }
                else
                {
                    medicine.IsDeleted = true;
                    medicine.DeletedAt = DateTime.Now;
                    medicine.DeletedBy = userId;
                    await _medicineRepo.ModifyMedicineById(medicine);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /*Deprecated*/
        //private void ValidateMedicineForm(MedicineForm medicineForm)
        //{
        //    var regex = new Regex(@"^TH[0-9]{8}$");
        //    if (!regex.IsMatch(medicineForm.MedicineId))
        //    {
        //        throw new Exception("MedicineId format is invalid");
        //    }
        //}

        private async Task<string> AutoGenerateNewMedicineId()
        {
            try
            {
                var lastMedicineId = await _medicineRepo.GetLastMedicineId();
                var newMedicineId = int.Parse(lastMedicineId.Substring(2, 8)) + 1;
                return "TH" + newMedicineId.ToString("D8");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region medicine_group
        public async Task<List<MedicineGroup>> GetMedicineGroups()
        {
            try
            {
                return await _medicineGroupRepo.GetMedicineGroups();
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
                var checkMedicineGroupIsExisted = await _medicineGroupRepo.GetMedicineGroupByName(medicineGroupForm.Name);
                if (checkMedicineGroupIsExisted.Any())
                {
                    throw new Exception("Medicine group name already exists");
                }
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
                return await _medicineGroupRepo.CreateMedicineGroup(medicineGroup);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task DeleteMedicineGroupById(string id, string userId)
        {
            var medicineGroup = await _medicineGroupRepo.GetMedicineGroupById(id);
            if (medicineGroup == null)
            {
                throw new Exception("Medicine group not found");
            }
            else
            {
                medicineGroup.IsDeleted = true;
                medicineGroup.DeletedAt = DateTime.Now;
                medicineGroup.DeletedBy = userId;
                await _medicineGroupRepo.ModifyMedicineGroupById(medicineGroup);
            }
        }

        public async Task UpdateMedicineGroupById(string id, MedicineGroupForm medicineGroupForm, string userId)
        {
            var medicineGroup = await _medicineGroupRepo.GetMedicineGroupById(id);
            var medicines = await _medicineRepo.GetMedicineByGroupName(medicineGroup.Name);
            if (medicineGroup == null)
            {
                throw new Exception("Medicine group not found");
            }
            else if (!medicines.Any())
            {
                throw new Exception("List of medicines not found");
            }
            else
            {
                medicineGroup.Name = medicineGroupForm.Name;
                medicineGroup.UpdatedAt = DateTime.Now;
                medicineGroup.UpdatedBy = userId;
                foreach (var medicine in medicines)
                {
                    medicine.GroupName = medicineGroupForm.Name;
                    await _medicineRepo.ModifyMedicineById(medicine);
                }
                await _medicineGroupRepo.ModifyMedicineGroupById(medicineGroup);
            }
        }
        #endregion
    }
}
