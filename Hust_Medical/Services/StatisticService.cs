using System.Data;
using System.Globalization;

namespace Hust_Medical.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IPatientService _patientService;
        private readonly IPrescriptionService _prescriptionService;

        public StatisticService(IPatientService patientService, IPrescriptionService prescriptionService)
        {
            _patientService = patientService;
            _prescriptionService = prescriptionService;
        }

        public byte[] ExportStatistic(DateTime startTime, DateTime endTime)
        {
			try
            {
                using (var stream = new MemoryStream())
                {
                    //Initialize Workbook, Worksheets
                    Workbook workbook = new Workbook();
                    workbook.LoadFromFile($"{Directory.GetCurrentDirectory()}{@"\wwwroot\reportTemplate\Báo cáo thống kê tổng hợp.xlsx"}");
                    Worksheet patientSheet = workbook.Worksheets[0];
                    Worksheet revenueSheet = workbook.Worksheets[1];

                    //Initialize data
                    List<DateTime> dateList = new List<DateTime>();
                    List<long> numOfPatient = new List<long>();
                    List<long> revenueTotal = new List<long>();

                    //Get date range
                    dateList = Enumerable.Range(0, 1 + endTime.Subtract(startTime).Days)
                        .Select(offset => startTime.AddDays(offset))
                        .ToList();

                    //Get number of patient by date
                    for(int i = 0; i < dateList.Count; i++)
                    {
                        numOfPatient.Add(_patientService.GetNumberPatientsByCreatedDay(dateList[i]).Result);
                    }

                    //Get revenue by date
                    for(int i = 0; i < dateList.Count; i++)
                    {
                        revenueTotal.Add(_prescriptionService.GetRevenueMedicinePrescribedByCreatedDay(dateList[i]).Result);
                    }

                    //Fill data to sheet 1
                    patientSheet.Workbook.MarkerDesigner.AddParameter("PatientDay", dateList.Select(d => d.Date.ToString("dd/MM/yyyy")).ToList());
                    patientSheet.Workbook.MarkerDesigner.AddParameter("NumOfPatient", numOfPatient);
                    patientSheet.Workbook.MarkerDesigner.AddParameter("PatientStartTime", startTime.ToString("dd/MM/yyyy"));
                    patientSheet.Workbook.MarkerDesigner.AddParameter("PatientEndTime", endTime.ToString("dd/MM/yyyy"));

                    //Fill data to sheet 2
                    revenueSheet.Workbook.MarkerDesigner.AddParameter("RevenueDay", dateList.Select(d => d.Date.ToString("dd/MM/yyyy")).ToList());
                    revenueSheet.Workbook.MarkerDesigner.AddParameter("RevenueTotal", revenueTotal);
                    revenueSheet.Workbook.MarkerDesigner.AddParameter("RevenueStartTime", startTime.ToString("dd/MM/yyyy"));
                    revenueSheet.Workbook.MarkerDesigner.AddParameter("RevenueEndTime", endTime.ToString("dd/MM/yyyy"));

                    //Apply data
                    patientSheet.Workbook.MarkerDesigner.Apply();
                    revenueSheet.Workbook.MarkerDesigner.Apply();

                    //Save excel file
                    workbook.SaveToStream(stream);
                    workbook.Dispose();
                    return stream.ToArray();
                }

            }
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}
