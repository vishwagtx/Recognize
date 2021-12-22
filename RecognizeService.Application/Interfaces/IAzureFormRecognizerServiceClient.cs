using Azure.AI.FormRecognizer.Models;

namespace RecognizeService.Application.Interfaces
{
    public interface IAzureFormRecognizerServiceClient
    {
        Task<FormPageCollection> ExtractContent(Stream stream);
    }
}
