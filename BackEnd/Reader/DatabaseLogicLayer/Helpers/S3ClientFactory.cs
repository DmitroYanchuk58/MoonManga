using Amazon.S3;
using DatabaseAccessLayer.Repositories;
using DatabaseAccessLayer.Services;
using DatabaseLogicLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLogicLayer.Helpers
{
    public static class S3ClientFactory
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAmazonS3>(sp =>
            {
                var serviceUrl = configuration["Minio:ServiceUrl"] ?? "http://localhost:9000";
                var accessKey = configuration["Minio:AccessKey"] ?? "minioadmin";
                var secretKey = configuration["Minio:SecretKey"] ?? "minioadminpassword";

                var config = new AmazonS3Config
                {
                    ServiceURL = serviceUrl,
                    ForcePathStyle = true
                };

                return new AmazonS3Client(accessKey, secretKey, config);
            });

            services.AddScoped(typeof(ICRUD_Repository<>), typeof(CRUD_Repository<>));
            services.AddScoped<IStorage_Repository, MinioStorage_Repository>();

            return services;
        }
    }
}
