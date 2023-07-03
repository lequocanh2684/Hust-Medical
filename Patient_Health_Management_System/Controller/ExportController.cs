namespace Patient_Health_Management_System.Controller
{
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        public ExportController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet("/api/export/medicines")]
        public async Task<IActionResult> ExportMedicines()
        {
            var medicines = await _medicineService.GetMedicines();
            var fileContents = _medicineService.ExportToExcel(medicines);
            var response = File(fileContents,
                               "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                              "Báo cáo xuất kho thuốc ngày " + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".xlsx");
            return response;
        }
    }
}
