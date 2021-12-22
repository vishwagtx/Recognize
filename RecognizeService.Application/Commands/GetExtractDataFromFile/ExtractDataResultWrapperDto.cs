namespace RecognizeService.Application.Commands.GetExtractDataFromFile
{
    public class ExtractDataResultWrapperDto
    {
        public bool Succeed { get; set; } = true;
        public string ErrorMessage { get; set; }
        public ExtractDataResultDto Result { get; set; }
    }
}
