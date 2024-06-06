using AMS.Application.Dtos.Excel;

namespace AMS.Infrastructure.Services.Excel
{
    public class AccelerationExcelReader : ExcelRegister<AccelerationResponseDto>
    {
        public override AccelerationResponseDto ExecuteExcelReader(Stream file)
        {
            throw new NotImplementedException();
        }
    }
}
