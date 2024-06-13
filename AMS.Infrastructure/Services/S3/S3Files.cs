using Amazon.S3;
using Amazon.S3.Model;
using AMS.Application.Dtos.S3;
using AMS.Application.Interfaces.Services;
using AMS.Infrastructure.Commons.AWS;

namespace AMS.Infrastructure.Services.S3
{
    public class S3Files : IS3Files
    {
        private readonly AmazonS3Client _amazonS3Client = new(AwsCredentials.AwsPublicKey, AwsCredentials.AwsSecretKey,
            Amazon.RegionEndpoint.USEast2);

        public async Task<bool> UploadFileAsync(FileS3Dto fileS3Dto)
        {
            var key = string.IsNullOrEmpty(fileS3Dto.Prefix)
                ? fileS3Dto.Key
                : $"{fileS3Dto.Prefix.TrimEnd('/')}/{fileS3Dto.Key}";
            var request = new PutObjectRequest
            {
                BucketName = fileS3Dto.BuckName,
                Key = key,
                InputStream = fileS3Dto.InputStream!.OpenReadStream(),
                ContentType = fileS3Dto.ContentType
            };

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
    }
}
