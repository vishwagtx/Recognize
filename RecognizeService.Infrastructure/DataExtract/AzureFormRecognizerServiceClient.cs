using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Microsoft.Extensions.Configuration;
using RecognizeService.Application.Interfaces;

namespace RecognizeService.Infrastructure.DataExtract
{
    internal class AzureFormRecognizerServiceClient : IAzureFormRecognizerServiceClient
    {
        readonly FormRecognizerClient recognizerClient;

        public AzureFormRecognizerServiceClient(IConfiguration configuration)
        {
            string apiKey = configuration["FormRecognizer:ApiKey"];
            string endPoint = configuration["FormRecognizer:Endpoint"];

            AzureKeyCredential credential = new AzureKeyCredential(apiKey);

            recognizerClient = new FormRecognizerClient(new Uri(endPoint), credential);
        }

        public async Task<FormPageCollection> ExtractContent(Stream stream)
        {
            return await recognizerClient
                .StartRecognizeContentAsync(stream)
                .WaitForCompletionAsync();
        }
    }
}
