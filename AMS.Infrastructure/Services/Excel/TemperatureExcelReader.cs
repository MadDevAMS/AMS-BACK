using System.Text.RegularExpressions;
using AMS.Application.Dtos.Excel;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using static AMS.Infrastructure.Commons.Commons.ExcelResources;


namespace AMS.Infrastructure.Services.Excel
{
    public class TemperatureExcelReader : ExcelRegister<TemperatureExcelResponseDto>
    {
        public override TemperatureExcelResponseDto ExecuteExcelReader(IFormFile file)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelPackage = new ExcelPackage(file.OpenReadStream());
            var workSheet = excelPackage.Workbook.Worksheets[0] ?? throw new Exception(WORKSHEET_ERROR);

            using var headers = workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column];

            var lastRow = workSheet.Dimension.End.Row;
            var desiredHeaders = new string[] { TIMESTAMP, VALUE, MEASUREMENT_TYPE, AXIS, AXIS_LABEL, SPOT_DYID, SPOT_NAME, SPOT_TYPE, SPOT_RPM, SPOT_POWER, SPOT_MODEL, MACHINE_ID, MACHINE_NAME, BATTERY_LEVEL, TELEMETRY_INTERVAL, INTERVAL_UNIT, DYNAMIC_RANGE_IN_G, DATA_SOURCE, SPOT_PATH };

            if (!desiredHeaders.All(e => headers.Any(h => h.Value.Equals(e))))
            {
                throw new Exception(WORKSHEET_ERROR);
            }

            string timestamp, value, measurement_type, axis, axis_label, spot_id, spot_dyid, spot_name, spot_type, spot_rpm, spot_power, spot_model, machine_id, machine_name;

            timestamp = Regex.Replace(headers.First(h => h.Value.Equals(TIMESTAMP)).Address.ToString(), @"([0-9])", string.Empty);
            value = Regex.Replace(headers.First(h => h.Value.Equals(VALUE)).Address.ToString(), @"([0-9])", string.Empty);
            measurement_type = Regex.Replace(headers.First(h => h.Value.Equals(MEASUREMENT_TYPE)).Address.ToString(), @"([0-9])", string.Empty);
            axis = Regex.Replace(headers.First(h => h.Value.Equals(AXIS)).Address.ToString(), @"([0-9])", string.Empty);
            axis_label = Regex.Replace(headers.First(h => h.Value.Equals(AXIS_LABEL)).Address.ToString(), @"([0-9])", string.Empty);
            spot_id = Regex.Replace(headers.First(h => h.Value.Equals(SPOT_ID)).Address.ToString(), @"([0-9])", string.Empty);
            spot_dyid = Regex.Replace(headers.First(h => h.Value.Equals(SPOT_DYID)).Address.ToString(), @"([0-9])", string.Empty);
            spot_name = Regex.Replace(headers.First(h => h.Value.Equals(SPOT_NAME)).Address.ToString(), @"([0-9])", string.Empty);
            spot_type = Regex.Replace(headers.First(h => h.Value.Equals(SPOT_TYPE)).Address.ToString(), @"([0-9])", string.Empty);
            spot_rpm = Regex.Replace(headers.First(h => h.Value.Equals(SPOT_RPM)).Address.ToString(), @"([0-9])", string.Empty);
            spot_power = Regex.Replace(headers.First(h => h.Value.Equals(SPOT_POWER)).Address.ToString(), @"([0-9])", string.Empty);
            spot_model = Regex.Replace(headers.First(h => h.Value.Equals(SPOT_MODEL)).Address.ToString(), @"([0-9])", string.Empty);
            machine_id = Regex.Replace(headers.First(h => h.Value.Equals(MACHINE_ID)).Address.ToString(), @"([0-9])", string.Empty);
            machine_name = Regex.Replace(headers.First(h => h.Value.Equals(MACHINE_NAME)).Address.ToString(), @"([0-9])", string.Empty);

            var response = new TemperatureExcelResponseDto
            {
                MeasurementType = workSheet.Cells[measurement_type + 2].Value?.ToString()!,
                SpotId = workSheet.Cells[spot_id + 2].Value?.ToString()!,
                SpotDyid = workSheet.Cells[spot_dyid + 2].Value?.ToString()!,
                SpotName = workSheet.Cells[spot_name + 2].Value?.ToString()!,
                SpotType = workSheet.Cells[spot_type + 2].Value?.ToString()!,
                SpotRpm = workSheet.Cells[spot_rpm + 2].Value?.ToString()!,
                SpotModel = workSheet.Cells[spot_model + 2].Value?.ToString()!,
                MachineId = workSheet.Cells[machine_id + 2].Value?.ToString()!,
                MachineName = workSheet.Cells[machine_name + 2].Value?.ToString()!,
            };

            for (var row = 2; row <= lastRow; row++)
            {
                if (!workSheet.Cells[row, 1, row, workSheet.Dimension.End.Column].Any(c => c.Text != "")) break;

                var timeStamp = workSheet.Cells[timestamp + row].Value?.ToString()!;
                var valueData = float.Parse(workSheet.Cells[value + row].Value?.ToString()!);

                response.Values.Add(valueData);
                response.TimeStamp.Add(DateTimeOffset.Parse(timeStamp));
            }

            return response;
        }
    }
}
