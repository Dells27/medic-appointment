using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientService.Application.Interfaces;

namespace PatientService.Application.Interfaces
{
    // Contrato para interactuar con AWS S3
    // Application no sabe que existe S3 — solo sabe que existe algo que sube archivos
    public interface IS3Service
    {
        // Sube un archivo a S3 y devuelve el key del archivo
        Task<string> UploadFileAsync(Stream FileStream, string FileName, string ContentType);

        // Genera una URL pre-firmada para descargar el archivo (válida por 1 hora)
        Task<string> GeneratePresignedUrlAsync(string FileKey);

        // Elimina un archivo de S3
        Task DeleteFileAsync(string FileKey);
    }
}
