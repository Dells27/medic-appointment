using PatientService.Application.DTOs;
using PatientService.Application.Interfaces;
using PatientService.Domain.Entities;

namespace PatientService.Application.UseCases.UploadDocument;

public class UploadDocumentHandler
{
    private readonly IPatientRepository _patientRepository;
    private readonly IS3service _s3Service;

    public UploadDocumentHandler(IPatientRepository patientRepository, IS3service s3Service)
    {
        _patientRepository = patientRepository;
        _s3Service = s3Service;
    }

    public async Task<MedicalDocumentResponse> Handle(Guid userId, Stream fileStream, string fileName, string contentType, string documentType)
    {
        // Verificar que el paciente existe
        var patient = await _patientRepository.GetByIdAsync(userId);
        if (patient is null)
            throw new Exception("Perfil de paciente no encontrado");

        // Subir archivo a S3
        var fileKey = await _s3Service.UploadFileAsync(fileStream, fileName, contentType);

        // Crear el documento médico en la BD
        var document = MedicalDocument.Create(
            patient.id,
            fileName,
            fileKey,
            contentType,
            fileStream.Length,
            documentType);

        patient.Document.ToList(); // Cargar documentos
        // Generar URL pre-firmada para descarga
        var downloadUrl = await _s3Service.GeneratePresignedUrlAsync(fileKey);

        await _patientRepository.SaveChangesAsync();

        return new MedicalDocumentResponse
        {
            Id = document.id,
            fileName = document.fileName,
            fileType = document.fileType,
            documentType = document.documentType,
            fileSize = document.fileSize,
            upLoadedAt = document.uploadedAt,
            downLoadUrl = downloadUrl
        };
    }
}