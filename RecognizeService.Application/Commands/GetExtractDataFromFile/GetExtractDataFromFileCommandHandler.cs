using MediatR;
using RecognizeService.Application.ML;
using RecognizeService.Application.Interfaces;
using RecognizeService.Application.Common.Dtos;

namespace RecognizeService.Application.Commands.GetExtractDataFromFile
{
    public class GetExtractDataFromFileCommandHandler : IRequestHandler<GetExtractDataFromFileCommand, ExtractDataResultWrapperDto>
    {
        readonly IAzureFormRecognizerServiceClient client;
        readonly ITrainDataClient trainDataClient;

        public GetExtractDataFromFileCommandHandler(IAzureFormRecognizerServiceClient client,
            ITrainDataClient trainDataClient)
        {
            this.client = client;
            this.trainDataClient = trainDataClient;
        }

        public async Task<ExtractDataResultWrapperDto> Handle(GetExtractDataFromFileCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await client.ExtractContent(request.File.OpenReadStream());

            var trainDataSet = trainDataClient.GetTrainModel();

            var extactedDataResult = DataExtractML.Extract(trainDataSet, result[0].Lines);

            if (!extactedDataResult.ValidDocument)
            {
                return new ExtractDataResultWrapperDto
                {
                    Succeed = false,
                    ErrorMessage = "Please provide a valid National ID document."
                };
            }

            var extactedData = extactedDataResult.Values;

            var sex = extactedData["Sex"]?.ToString() ?? "";
            string salutation = "";

            if (!string.IsNullOrEmpty(sex)) salutation = sex.Contains("M") ? "Mr" : (sex.Contains("F") ? "Miss" : "Other");

            return new ExtractDataResultWrapperDto {
                Result = new ExtractDataResultDto
            {
                DocumentNumber = extactedData["Number"]?.ToString() ?? "",
                FullName = extactedData["Name"]?.ToString() ?? "",
                Sex = sex,
                DOB = CustomDateDto.ConvertToCustomDate(extactedData["DOB"] != null ? (DateTime)extactedData["DOB"] : null),
                Address = (string[])extactedData["Address"],
                Salutation = salutation,
                Nationality = extactedData["Nationality"]?.ToString() ?? "",
            } };
        }
    }
}
