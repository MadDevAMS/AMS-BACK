using AMS.Application.Dtos.Excel;

namespace AMS.Infrastructure.Services.Excel
{
    public class VelocityExcelReader : ExcelRegister<VelocityResponseDto>
    {
        public override VelocityResponseDto ExecuteExcelReader(Stream file)
        {
            throw new NotImplementedException();
        }
    }
}
