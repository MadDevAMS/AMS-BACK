using Amazon.S3;
using Amazon.S3.Model;
using AMS.Application;
using AMS.Application.Dtos.S3;
using AMS.Application.Interfaces.Services;
using AMS.Infrastructure.Commons.AWS;
using Microsoft.AspNetCore.Http;

namespace AMS.Infrastructure.Services.S3
{
    public class S3Files : IS3Files
    {
        private readonly AmazonS3Client _amazonS3Client = new(AwsCredentials.AwsPublicKey, AwsCredentials.AwsSecretKey,
            Amazon.RegionEndpoint.USEast1);

        public async Task<bool> UploadFileAsync(string bucketName, string prefix, IFormFile file)
        {
            var fileName = $"{prefix}/{file.FileName}";
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = fileName,
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            var response = await _amazonS3Client.PutObjectAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<string> DownloadFileAsync(FileS3Dto fileS3Dto)
        {
            var request = new GetObjectRequest
            {
                BucketName = fileS3Dto.BuckName,
                Key = fileS3Dto.Key
            };

            using var response = await _amazonS3Client.GetObjectAsync(request);

            var responseStream = response.ResponseStream;
            using var memoryStream = new MemoryStream();
            await responseStream.CopyToAsync(memoryStream);

            var bytes = memoryStream.ToArray();
            var fileBase64 = Convert.ToBase64String(bytes);

            return fileBase64;
        }

        public async Task<bool> DeleteFileAsync(FileS3Dto fileS3Dto)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = fileS3Dto.BuckName,
                Key = fileS3Dto.Key
            };

            var response = await _amazonS3Client.DeleteObjectAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public async Task<string> GetFileAsync(string bucketName, string prefix, string fileName)
        {
            var fileKey = $"{prefix}/{fileName}";
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = fileKey,
                Expires = DateTime.UtcNow.AddMinutes(5)
            };

            return await _amazonS3Client.GetPreSignedURLAsync(request);
        }

        public async Task<IEnumerable<S3ObjectDto>> GetFilesEntidadAsync(string bucketName, string prefix)
        {

            var request = new ListObjectsV2Request()
            {
                BucketName = bucketName,
                Prefix = prefix
            };
            var result = await _amazonS3Client.ListObjectsV2Async(request);
            var s3Objects = result.S3Objects.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = s.Key,
                    Expires = DateTime.UtcNow.AddDays(1)
                };
                return new S3ObjectDto()
                {
                    Name = s.Key.ToString(),
                    PresigneUrl = _amazonS3Client.GetPreSignedURL(urlRequest),
                    Size = s.Size.ToString()
                };
            });

            return s3Objects;
        }
    }
}
