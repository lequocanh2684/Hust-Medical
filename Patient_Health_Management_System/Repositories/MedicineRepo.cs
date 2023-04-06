namespace Patient_Health_Management_System.Repositories
{
    public class MedicineRepo
    {
        private readonly IMongoCollection<Medicine> _medicines;

        public MedicineRepo(MongoDbSetup dbSetup)
        {
            _medicines = dbSetup.GetDatabase().GetCollection<Medicine>("medicine");
        }

        public  async Task<List<Medicine>> GetMedicines()
        {
            return await _medicines.Find(medicine => true).ToListAsync();
        }

        public async Task<Medicine> GetMedicineById(string id)
        {
            return await _medicines.Find(medicine => medicine.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Medicine> GetMedicineByName(string name)
        {
            return await _medicines.Find(medicine => medicine.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Medicine> CreateMedicine(Medicine medicine)
        {
            await _medicines.InsertOneAsync(medicine);
            return medicine;
        }

        public async Task UpdateMedicine(string id, Medicine medicine)
        {
            await _medicines.ReplaceOneAsync(m => m.Id == id, medicine);
        }

        public async Task DeleteMedicine(string id)
        {
            await _medicines.DeleteOneAsync(medicine => medicine.Id == id);
        }
    }
}
