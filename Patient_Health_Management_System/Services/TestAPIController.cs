using Microsoft.AspNetCore.Mvc;

namespace Patient_Health_Management_System.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAPIController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public TestAPIController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }
        // GET: api/TestAPI
        [HttpGet("/api/medicines", Name = "GetMedicines")]
        public async Task<List<Medicine>> GetMedicines()
        {
            return await _medicineService.GetMedicines();
        }

        [HttpGet("/api/medicines/{page}/{pageSize}", Name = "GetMedicinesByPage")]
        public async Task<List<Medicine>> GetMedicinesByPage(int page, int pageSize)
        {
            return await _medicineService.GetMedicinesByPage(page, pageSize);
        }

        [HttpGet("/api/medicines/{id}", Name = "GetMedicineById")]
        public async Task<Medicine> GetMedicineById(string id)
        {
            return await _medicineService.GetMedicineById(id);
        }

        [HttpGet("/api/medicines/name/{name}", Name = "GetMedicineByName")]
        public async Task<List<Medicine>> GetMedicineByName(string keyword)
        {
            return await _medicineService.GetMedicineByKeyword(keyword);
        }

        [HttpGet("/api/medicines/group/{groupName}", Name = "GetMedicineByGroupName")]
        public async Task<List<Medicine>> GetMedicineByGroupName(string groupName)
        {
            return await _medicineService.GetMedicineByGroupName(groupName);
        }

        [HttpPost("/api/medicines", Name = "CreateMedicine")]
        public async Task<Medicine> CreateMedicine([FromBody]MedicineForm medicineForm, [FromQuery]string userId)
        {
            return await _medicineService.CreateMedicine(medicineForm, userId);
        }

        [HttpPut("/api/medicines/{id}", Name = "UpdateMedicineById")]
        public async Task UpdateMedicineById(string id, [FromBody]MedicineForm medicineForm, [FromQuery]string userId)
        {
            await _medicineService.UpdateMedicineById(id, medicineForm, userId);
        }

        [HttpDelete("/api/medicines/{id}", Name = "DeleteMedicineById")]
        public async Task DeleteMedicineById(string id, [FromQuery]string userId)
        {
            await _medicineService.DeleteMedicineById(id, userId);
        }
    }
}
