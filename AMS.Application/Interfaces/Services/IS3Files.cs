using AMS.Application.Dtos.S3;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.Interfaces.Services
{
    public interface IS3Files
    {
        Task<bool> UploadFileAsync(string bucketName, string prefix, IFormFile file);
        Task<string> DownloadFileAsync(FileS3Dto fileS3Dto);
        Task<bool> DeleteFileAsync(FileS3Dto fileS3Dto);
        Task<string> GetFileAsync(string bucketName, string prefix, string fileName);
    }
}
