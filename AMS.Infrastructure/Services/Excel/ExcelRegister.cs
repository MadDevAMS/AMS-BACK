using Microsoft.AspNetCore.Http;

namespace AMS.Infrastructure.Services.Excel
{
    public abstract class ExcelRegister<T>
    {
        public abstract T ExecuteExcelReader(IFormFile file);
    }
}
