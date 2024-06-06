using AMS.Application.Dtos.Excel;

namespace AMS.Infrastructure.Services.Excel
{
    public class TemperatureExcelReader : ExcelRegister<TemperatureResponseDto>
    {
        public override TemperatureResponseDto ExecuteExcelReader(Stream file)
        {
            throw new NotImplementedException();
        }
    }
}
