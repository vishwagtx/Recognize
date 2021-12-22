using RecognizeService.Application.Common.Dtos;

namespace RecognizeService.Application.Commands.GetExtractDataFromFile
{
    public class ExtractDataResultDto
    {
        public bool IsTemporary { get; set; } = false;
        public bool IsDocumentExpired { get; set; } = false;
        public string DocumentNumber { get; set; }
        public string AlternateDocument { get; set; }
        public string Nationality { get; set; }
        public string Salutation { get; set; }
        public string FullName { get; set; }
        public string CommonName { get; set; }
        public string Sex { get; set; }
        public CustomDateDto DOB { get; set; }
        public string[] Address { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public double FrontSideAngle { get; set; }
        public double BackSideAngle { get; set; }
    }
}
