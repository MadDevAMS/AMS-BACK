using AMS.Application.Dtos.S3;

namespace AMS.Application.Interfaces.Services
{
    public interface IS3Files
    {
        Task<bool> UploadFileAsync(FileS3Dto fileS3Dto);
        Task<string> DownloadFileAsync(FileS3Dto fileS3Dto);
        Task<bool> DeleteFileAsync(FileS3Dto fileS3Dto);
    }
}
