using RecognizeService.Application.Interfaces;
using RecognizeService.Domain.TrainSet;
using System.Text.Json;

namespace RecognizeService.Infrastructure.TrainData
{
    internal class TrainDataClient : ITrainDataClient
    {
        public TrainModelResult GetTrainModel()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var jsonString = File.ReadAllText("Json\\train.json");
            return JsonSerializer.Deserialize<TrainModelResult>(jsonString, options);
        }
    }
}
