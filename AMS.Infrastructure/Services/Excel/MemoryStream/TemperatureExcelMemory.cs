using AMS.Application.Dtos.Excel;
using OfficeOpenXml;
using static AMS.Infrastructure.Commons.Commons.ExcelResources;


namespace AMS.Infrastructure;

public class TemperatureExcelMemory : ExcelBuilderMemory<TemperatureExcelResponseDto>
{
    public override TemperatureExcelResponseDto ExecuteExcelReaderMemory(MemoryStream file)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var excelPackage = new ExcelPackage(file);
        var workSheet = excelPackage.Workbook.Worksheets[0] ?? throw new Exception(WORKSHEET_ERROR);

        using var headers = workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column];

        var lastRow = workSheet.Dimension.End.Row;
        var headerAddresses = GetHeadersExcel(headers);

        var response = new TemperatureExcelResponseDto
        {
            MeasurementType = workSheet.Cells[headerAddresses[MEASUREMENT_TYPE] + 2].Value?.ToString()!,
            SpotId = workSheet.Cells[headerAddresses[SPOT_ID] + 2].Value?.ToString()!,
            SpotDyid = workSheet.Cells[headerAddresses[SPOT_DYID] + 2].Value?.ToString()!,
            SpotName = workSheet.Cells[headerAddresses[SPOT_NAME] + 2].Value?.ToString()!,
            SpotType = workSheet.Cells[headerAddresses[SPOT_TYPE] + 2].Value?.ToString()!,
            SpotRpm = workSheet.Cells[headerAddresses[SPOT_RPM] + 2].Value?.ToString()!,
            SpotModel = workSheet.Cells[headerAddresses[SPOT_MODEL] + 2].Value?.ToString()!,
            MachineId = workSheet.Cells[headerAddresses[MACHINE_ID] + 2].Value?.ToString()!,
            MachineName = workSheet.Cells[headerAddresses[MACHINE_NAME] + 2].Value?.ToString()!
        };

        for (var row = 2; row <= lastRow; row++)
        {
            if (!workSheet.Cells[row, 1, row, workSheet.Dimension.End.Column].Any(c => c.Text != "")) break;

            var timeStamp = workSheet.Cells[headerAddresses[TIMESTAMP] + row].Value?.ToString()!;
            var valueData = float.Parse(workSheet.Cells[headerAddresses[VALUE] + row].Value?.ToString()!);

            response.Values.Add(valueData);
            response.TimeStamp.Add(DateTimeOffset.Parse(timeStamp));
        }

        return response;
    }
}
