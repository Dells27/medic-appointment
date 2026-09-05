using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Entities
{
    public class MedicalDocument
    {

        public Guid id { get; private set; }
        public Guid patientId { get; private set; }
        public string fileName { get; private set; } = null!;
        public string fileKey { get; private set; } = null!; //S3 Key
        public string fileType { get; private set; } = null!; //.PDF, .JPG, .PNG
        public long fileSize { get; private set; } //Bytes size

        public string documentType { get; private set; } = null!; //Examan, Recipe, etc

        public DateTime uploadedAt { get; private set; }

        private MedicalDocument() { }

        public static MedicalDocument Create(Guid PatientId, string FileName, string FileKey, string FileType, long FileSize, string DocumentType)
        {
            return new MedicalDocument()
            {
                patientId = PatientId,
                fileName = FileName,
                fileKey = FileKey,
                fileType = FileType,
                fileSize = FileSize,
                documentType = DocumentType,
                uploadedAt = DateTime.UtcNow

            };
    }
    }
}
