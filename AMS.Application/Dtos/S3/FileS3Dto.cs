using Microsoft.AspNetCore.Http;

namespace AMS.Application.Dtos.S3
{
    public class FileS3Dto
    {
        public string? BuckName { get; set; }
        public string Key { get; set; } = null!;
        public string? Prefix { get; set; }
        public IFormFile InputStream { get; set; } = null!;
        public string? ContentType { get; set; }
    }
}
