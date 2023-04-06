namespace Patient_Health_Management_System.Services
{
    public class MedicineService
    {
        private readonly MedicineRepo _medicineRepo;

        public MedicineService(MedicineRepo medicineRepo)
        {
            _medicineRepo = medicineRepo;
        }

        public async Task<List<Medicine>> GetMedicines()
        {
            return await _medicineRepo.GetMedicines();
        }

        public async Task<Medicine> GetMedicineById(string id)
        {
            return await _medicineRepo.GetMedicineById(id);
        }

        public async Task<Medicine> GetMedicineByName(string name)
        {
            return await _medicineRepo.GetMedicineByName(name);
        }

        public async Task<Medicine> CreateMedicine(Medicine medicine)
        {

            return await _medicineRepo.CreateMedicine(medicine);
        }
    }
}
