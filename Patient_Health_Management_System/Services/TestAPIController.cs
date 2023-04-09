using Microsoft.AspNetCore.Mvc;

namespace Patient_Health_Management_System.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAPIController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        private readonly DiseaseService _diseaseService;

        public TestAPIController(IMedicineService medicineService, DiseaseService diseaseService)
        {
            _medicineService = medicineService;
            _diseaseService = diseaseService;
        }

        #region medicine
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

        [HttpGet("/api/medicines/name = {name}", Name = "GetMedicinesByName")]
        public async Task<List<Medicine>> GetMedicinesByName(string name)
        {
            return await _medicineService.GetMedicinesByName(name);
        }

        [HttpGet("/api/medicines/keyword = {keyword}", Name = "GetMedicineByKeyword")]
        public async Task<List<Medicine>> GetMedicineByKeyword(string keyword)
        {
            return await _medicineService.GetMedicineByKeyword(keyword);
        }

        [HttpGet("/api/medicines/group/{groupName}", Name = "GetMedicineByGroupName")]
        public async Task<List<Medicine>> GetMedicineByGroupName(string groupName)
        {
            return await _medicineService.GetMedicineByGroupName(groupName);
        }

        [HttpPost("/api/medicines", Name = "CreateMedicine")]
        public async Task<Medicine> CreateMedicine([FromForm] MedicineForm medicineForm, [FromQuery] string userId)
        {
            return await _medicineService.CreateMedicine(medicineForm, userId);
        }

        [HttpPut("/api/medicines/{id}", Name = "UpdateMedicineById")]
        public async Task UpdateMedicineById(string id, [FromBody] MedicineForm medicineForm, [FromQuery] string userId)
        {
            await _medicineService.UpdateMedicineById(id, medicineForm, userId);
        }

        [HttpDelete("/api/medicines/{id}", Name = "DeleteMedicineById")]
        public async Task DeleteMedicineById(string id, [FromQuery] string userId)
        {
            await _medicineService.DeleteMedicineById(id, userId);
        }
        #endregion

        #region medicine_group
        [HttpGet("/api/medicineGroups", Name = "GetMedicineGroups")]
        public async Task<List<MedicineGroup>> GetMedicinesGroup()
        {
            return await _medicineService.GetMedicineGroups();
        }

        [HttpPost("/api/medicineGroups", Name = "CreateMedicineGroups")]
        public async Task<MedicineGroup> CreateMedicineGroup([FromBody] MedicineGroupForm medicineGroupForm, [FromQuery] string userId)
        {
            return await _medicineService.CreateMedicineGroup(medicineGroupForm, userId);
        }

        [HttpDelete("/api/medicineGroups/{id}", Name = "DeleteMedicineGroupById")]
        public async Task DeleteMedicineGroupById(string id, [FromQuery] string userId)
        {
            await _medicineService.DeleteMedicineGroupById(id, userId);
        }

        [HttpPut("/api/medicineGroups/{id}", Name = "UpdateMedicineGroupById")]
        public async Task UpdateMedicineGroupById(string id, [FromBody] MedicineGroupForm medicineGroupForm, [FromQuery] string userId)
        {
            await _medicineService.UpdateMedicineGroupById(id, medicineGroupForm, userId);
        }
        #endregion

        #region disease
        [HttpGet("/api/diseases", Name = "GetDiseases")]
        public async Task<List<Disease>> GetDiseases()
        {
            return await _diseaseService.GetDiseases();
        }

        [HttpGet("/api/diseases/{page}/{pageSize}", Name = "GetDiseasesByPage")]
        public async Task<List<Disease>> GetDiseasesByPage(int page, int pageSize)
        {
            return await _diseaseService.GetDiseasesByPage(page, pageSize);
        }

        [HttpGet("/api/diseases/{id}", Name = "GetDiseaseById")]
        public async Task<Disease> GetDiseaseById(string id)
        {
            return await _diseaseService.GetDiseaseById(id);
        }

        [HttpGet("/api/diseases/name = {name}", Name = "GetDiseasesByName")]
        public async Task<List<Disease>> GetDiseasesByName(string name)
        {
            return await _diseaseService.GetDiseasesByName(name);
        }

        [HttpPost("/api/disease", Name = "CreateDisease")]
        public async Task <Disease> CreateDisease([FromForm] DiseaseForm diseaseForm, [FromQuery] string userId)
        {
            return await _diseaseService.CreateDisease(diseaseForm, userId);
        }

        [HttpPut("/api/diseases/{id}", Name = "UpdateDisease")]
        public async Task UpdateDiseaseById(string id, [FromBody] DiseaseForm diseaseForm, [FromQuery] string userId)
        {
            await _diseaseService.UpdateDiseaseById(id, diseaseForm, userId);
        }

        [HttpDelete("/api/diseases/{id}", Name = "DeleteDiseaseById")]
        public async Task DeleteDiseaseById(string id, [FromQuery] string userId)
        {
            await _diseaseService.DeleteDiseaseById(id, userId);
        }
        #endregion
    }
}
