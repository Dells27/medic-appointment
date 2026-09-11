using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using PatientService.Application.Interfaces;

namespace PatientService.Infrastructure.Services
{
    public class S3Service : IS3Service
    {

        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;


        public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:BucketName"]!;
        }


        public async Task<string> UploadFileAsync(Stream FileStream, string FileName, string ContentType)
        {

            // Generamos un key único para el archivo en S3
            var fileKey = $"medical-documents/{Guid.NewGuid()}/{FileName}";

            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileKey,
                InputStream = FileStream,
                ContentType = ContentType
            };

            await _s3Client.PutObjectAsync(request);
            return fileKey;
        }


        public async Task<string> GeneratePresignedUrlAsync(string FileKey)
        {
            // La URL pre-firmada es válida por 1 hora
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = FileKey,
                Expires = DateTime.UtcNow.AddHours(1)

            };

            return await Task.FromResult(_s3Client.GetPreSignedURL(request));
        }

        public async Task DeleteFileAsync(string FileKey)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = FileKey
            };

            await _s3Client.DeleteObjectAsync(request);
        }


    }
}
