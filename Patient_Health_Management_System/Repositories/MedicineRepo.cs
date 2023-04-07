﻿namespace Patient_Health_Management_System.Repositories
{
    public class MedicineRepo : IMedicineRepo
    {
        private readonly IMongoCollection<Medicine> _medicines;
        private readonly IMongoCollection<MedicineGroup> _medicineGroups;

        public MedicineRepo(MongoDbSetup dbSetup)
        {
            _medicines = dbSetup.GetDatabase().GetCollection<Medicine>("medicine");
            _medicineGroups = dbSetup.GetDatabase().GetCollection<MedicineGroup>("medicine_group");
        }

        #region medicine
        public  async Task<List<Medicine>> GetMedicines()
        {
            return await _medicines.Find(medicine => true).ToListAsync();   
        }

        public async Task<List<Medicine>> GetMedicinesByPage(int page, int pageSize)
        {
            return await _medicines.Find(medicine => true).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        public async Task<Medicine> GetMedicineById(string id)
        {
            return await _medicines.Find(medicine => medicine.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Medicine>> GetMedicineByKeyword(string keyword)
        {
            var filter = Builders<Medicine>.Filter.Text(keyword, new TextSearchOptions { CaseSensitive = false });
            return await _medicines.Find(filter).ToListAsync();
        }

        public async Task<Medicine> CreateMedicine(Medicine medicine)
        {
            await _medicines.InsertOneAsync(medicine);
            return medicine;
        }

        public async Task UpdateMedicineById(string id, Medicine medicine)
        {
            await _medicines.ReplaceOneAsync(m => m.Id == id, medicine);
        }

        public async Task DeleteMedicineById(string id, Medicine medicine)
        {
            await _medicines.ReplaceOneAsync(m => m.Id == id, medicine);
        }

        public async Task<List<Medicine>> GetMedicineByGroupName(string groupName)
        {
            return await _medicines.Find(medicine => medicine.GroupName == groupName).ToListAsync();
        }
        #endregion

        #region medicine_group
        public async Task<List<MedicineGroup>> GetMedicineGroups()
        {
            return await _medicineGroups.Find(medicineGroup => true).ToListAsync();
        }

        public async Task<MedicineGroup> GetMedicineGroupById(string id)
        {
            return await _medicineGroups.Find(medicineGroup => medicineGroup.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<MedicineGroup>> GetMedicineGroupByName(string name)
        {
            var filter = Builders<MedicineGroup>.Filter.Text(name, new TextSearchOptions { CaseSensitive = false });
            return await _medicineGroups.Find(filter).ToListAsync();
        }

        public async Task<MedicineGroup> CreateMedicineGroup(MedicineGroup medicineGroup)
        {
            await _medicineGroups.InsertOneAsync(medicineGroup);
            return medicineGroup;
        }

        public async Task DeleteMedicineGroupById(string id, MedicineGroup medicineGroup)
        {
            await _medicineGroups.ReplaceOneAsync(m => m.Id == id, medicineGroup);
        }

        public async Task UpdateMedicineGroupById(string id, MedicineGroup medicineGroup)
        {
            await _medicineGroups.ReplaceOneAsync(m => m.Id == id, medicineGroup);
        }
        #endregion
    }
}