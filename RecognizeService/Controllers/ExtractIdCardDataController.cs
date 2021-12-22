using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecognizeService.Application.Commands.GetExtractDataFromFile;

namespace RecognizeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtractIdCardDataController : ControllerBase
    {
        readonly IMediator bus;

        public ExtractIdCardDataController(IMediator bus)
        {
            this.bus = bus;
        }

        [HttpPost, Route("Upload")]
        public async Task<IActionResult> Upload()
        {
            var files = Request.Form.Files;

            if (files.Count == 0)
                return BadRequest("File is not found");

            var uploadResult = await bus.Send(new GetExtractDataFromFileCommand
            {
                File = files[0]
            });

            if (!uploadResult.Succeed) return BadRequest(uploadResult.ErrorMessage);

            return Ok(uploadResult.Result);
        }
    }
}
