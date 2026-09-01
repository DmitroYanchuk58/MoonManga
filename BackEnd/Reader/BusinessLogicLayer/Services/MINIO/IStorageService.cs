using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using System.Net;

namespace DatabaseAccessLayer.Services
{
    public interface IStorageService
    {
        public Task<string> UploadFileAsync(string bucketName, string key, Stream fileStream, string contentType);

        public Task<Stream?> GetFileAsync(string bucketName, string key);

        public Task DeleteFileAsync(string bucketName, string key);
    }
}