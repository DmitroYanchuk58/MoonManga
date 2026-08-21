using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using System.Net;

namespace DatabaseAccessLayer.Services
{
    public class MinioStorageService : IStorageService
    {
        private readonly IAmazonS3 _s3Client;

        public MinioStorageService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
        }

        public async Task<string> UploadFileAsync(string bucketName, string key, Stream fileStream, string contentType)
        {
            if (string.IsNullOrWhiteSpace(bucketName))
                throw new ArgumentException("Bucket name cannot be empty.", nameof(bucketName));

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Storage key cannot be empty.", nameof(key));

            ArgumentNullException.ThrowIfNull(fileStream);

            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists)
            {
                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };
                await _s3Client.PutBucketAsync(putBucketRequest);
            }

            var putObjectRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = fileStream,
                ContentType = contentType,
                AutoCloseStream = false
            };

            await _s3Client.PutObjectAsync(putObjectRequest);

            return key;
        }

        public async Task<Stream?> GetFileAsync(string bucketName, string key)
        {
            if (string.IsNullOrWhiteSpace(bucketName) || string.IsNullOrWhiteSpace(key))
                return null;

            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = key
                };

                var response = await _s3Client.GetObjectAsync(request);
                return response.ResponseStream;
            }
            catch (AmazonS3Exception ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task DeleteFileAsync(string bucketName, string key)
        {
            if (string.IsNullOrWhiteSpace(bucketName) || string.IsNullOrWhiteSpace(key))
                return;

            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);
        }
    }
}