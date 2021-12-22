using MediatR;
using Microsoft.AspNetCore.Http;

namespace RecognizeService.Application.Commands.GetExtractDataFromFile
{
    public class GetExtractDataFromFileCommand: IRequest<ExtractDataResultWrapperDto>
    {
        public IFormFile File { get; set; }
    }
}
