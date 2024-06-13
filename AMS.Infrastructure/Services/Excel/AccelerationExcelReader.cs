using AMS.Application.Dtos.Excel;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using static AMS.Infrastructure.Commons.Commons.ExcelResources;

namespace AMS.Infrastructure.Services.Excel
{
    public class AccelerationExcelReader : ExcelBuilder<AccelerationExcelResponseDto>
    {
        public override AccelerationExcelResponseDto ExecuteExcelReader(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelPackage = new ExcelPackage(file.OpenReadStream());
            var workSheet = excelPackage.Workbook.Worksheets[0] ?? throw new Exception(WORKSHEET_ERROR);

            using var headers = workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column];

            var lastRow = workSheet.Dimension.End.Row;
            var headerAddresses = GetHeadersExcel(headers);

            var response = new AccelerationExcelResponseDto
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

            float axisx = 0, axisy = 0, axisz = 0;

            for (var row = 2; row <= lastRow; row++)
            {
                if (!workSheet.Cells[row, 1, row, workSheet.Dimension.End.Column].Any(c => c.Text != "")) break;

                var timeStamp = workSheet.Cells[headerAddresses[TIMESTAMP] + row].Value?.ToString()!;
                var valueData = float.Parse(workSheet.Cells[headerAddresses[VALUE] + row].Value?.ToString()!);
                var axisData = workSheet.Cells[headerAddresses[AXIS] + row].Value?.ToString()!;

                switch (axisData)
                {
                    case AXIS_X:
                        axisx += valueData;
                        response.AxisX.Add(valueData);
                        response.TimeStamp.Add(DateTimeOffset.Parse(timeStamp));
                        break;
                    case AXIS_Y:
                        axisy += valueData;
                        response.AxisY.Add(valueData);
                        break;
                    case AXIS_Z:
                        axisz += valueData;
                        response.AxisZ.Add(valueData);
                        break;
                    default: break;
                }
            }

            float rms = (float)Math.Sqrt((axisx * axisx + axisy * axisy + axisz * axisz) / 3);

            response.Rms = rms;

            return response;
        }
    }
}
