namespace Patient_Health_Management_System.Controller
{
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        private readonly IBillingService _billingService;

        public ExportController(IMedicineService medicineService, IBillingService billingService)
        {
            _medicineService = medicineService;
            _billingService = billingService;
        }

        [HttpGet("/api/export/medicines")]
        public IActionResult ExportMedicines()
        {
            try
            {
                var fileContents = _medicineService.ExportToExcel();
                var response = File(fileContents,
                                   "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                                  "Báo cáo xuất kho thuốc ngày " + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-tt") + ".xlsx");
                return response;
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
