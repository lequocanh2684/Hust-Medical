namespace Hust_Medical.Controller
{
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        private readonly IStatisticService _statisticService;

        public ExportController(IMedicineService medicineService, IStatisticService statisticService)
        {
            _medicineService = medicineService;
            _statisticService = statisticService;
        }

        [HttpGet("medicines")]
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

        [HttpGet("statistic/{startTime}/{endTime}")]
        public IActionResult ExportStatistic([FromRoute] string startTime, [FromRoute] string endTime)
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

        [HttpGet("medicines/import/template")]
        public IActionResult CreateMedicineImportTemplate()
        {
            try
            {
                var fileContents = _medicineService.CreateImportTemplateFile();
                var response = File(fileContents,
                                   "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                                  "Mẫu nhập kho thuốc " + ".xlsx");
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
