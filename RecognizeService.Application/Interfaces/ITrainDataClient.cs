using RecognizeService.Domain.TrainSet;

namespace RecognizeService.Application.Interfaces
{
    public interface ITrainDataClient
    {
        TrainModelResult GetTrainModel();
    }
}
