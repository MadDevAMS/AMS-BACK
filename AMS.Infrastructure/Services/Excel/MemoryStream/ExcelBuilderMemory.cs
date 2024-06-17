using System.Text.RegularExpressions;
using AMS.Infrastructure.Commons.Commons;
using OfficeOpenXml;

namespace AMS.Infrastructure;

public abstract class ExcelBuilderMemory<T>
{
    public abstract T ExecuteExcelReaderMemory(MemoryStream file);

    protected Dictionary<string, string> GetHeadersExcel(ExcelRange headers)
    {
        var desiredHeaders = new string[]
        {
                ExcelResources.TIMESTAMP, ExcelResources.VALUE, ExcelResources.MEASUREMENT_TYPE, ExcelResources.AXIS,
                ExcelResources.AXIS_LABEL, ExcelResources.SPOT_ID, ExcelResources.SPOT_DYID, ExcelResources.SPOT_NAME,
                ExcelResources.SPOT_TYPE, ExcelResources.SPOT_RPM, ExcelResources.SPOT_POWER, ExcelResources.SPOT_MODEL,
                ExcelResources.MACHINE_ID, ExcelResources.MACHINE_NAME
        };

        var headerAddresses = new Dictionary<string, string>();

        foreach (var header in headers)
        {
            string headerValue = header.Value.ToString();
            if (desiredHeaders.Contains(headerValue))
            {
                headerAddresses[headerValue] = Regex.Replace(header.Address.ToString(), @"([0-9])", string.Empty);
            }
        }

        if (!desiredHeaders.All(e => headerAddresses.ContainsKey(e)))
        {
            throw new Exception(ExcelResources.WORKSHEET_ERROR);
        }

        return headerAddresses;
    }
}
