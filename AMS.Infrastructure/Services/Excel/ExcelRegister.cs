namespace AMS.Infrastructure.Services.Excel
{
    public abstract class ExcelRegister<T>
    {
        public abstract T ExecuteExcelReader(Stream file);
    }
}
