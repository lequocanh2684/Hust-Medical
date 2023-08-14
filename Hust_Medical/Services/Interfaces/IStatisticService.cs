using MudBlazor;

namespace Hust_Medical.Services.Interfaces
{ 
    public interface IStatisticService
    {
        byte[] ExportStatistic(DateTime startTime, DateTime endTime);
    }
}
