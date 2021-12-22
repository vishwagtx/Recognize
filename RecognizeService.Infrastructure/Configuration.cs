using Microsoft.Extensions.DependencyInjection;
using RecognizeService.Application.Interfaces;
using RecognizeService.Infrastructure.DataExtract;
using RecognizeService.Infrastructure.TrainData;

namespace RecognizeService.Infrastructure
{
    public static class Configuration
    {
        public static IServiceCollection AddInfraConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<ITrainDataClient, TrainDataClient>();
            services.AddSingleton<IAzureFormRecognizerServiceClient, AzureFormRecognizerServiceClient>();

            return services;
        }
    }
}
