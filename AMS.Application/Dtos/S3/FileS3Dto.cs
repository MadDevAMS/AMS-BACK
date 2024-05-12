namespace AMS.Application.Dtos.S3
{
    public class FileS3Dto
    {
        public string? BuckName { get; set; }
        public string Key { get; set; } = null!;
        public string? Prefix { get; set; }
        public Stream? InputStream { get; set; }
        public string? ContentType { get; set; }
    }
}
