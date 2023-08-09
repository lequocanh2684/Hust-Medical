namespace Hust_Medical.Controller
{
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        private readonly IBillingService _billingService;
        private readonly IStatisticService _statisticService;

        public ExportController(IMedicineService medicineService, IBillingService billingService, IStatisticService statisticService)
        {
            _medicineService = medicineService;
            _billingService = billingService;
            _statisticService = statisticService;
        }

        [HttpGet("medicines")]
        public IActionResult ExportMedicines()
        {
            try
            {
                var fileContents = _medicineService.ExportToExcel();
                //var fileContents = _statisticService.ExportStatistic(DateTime.Parse("04/21/2023"), DateTime.Parse("05/21/2023"));
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

        [HttpGet("statistic/{startTime}/{endTime}")]
        public IActionResult ExportStatistic([FromQuery] string startTime, [FromQuery] string endTime)
        {
            try
            {
                var fileContents = _statisticService.ExportStatistic(DateTime.Parse(startTime), DateTime.Parse(endTime));
                var response = File(fileContents,
                                   "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                                  "Báo cáo thống kê ngày " + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-tt") + ".xlsx");
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
