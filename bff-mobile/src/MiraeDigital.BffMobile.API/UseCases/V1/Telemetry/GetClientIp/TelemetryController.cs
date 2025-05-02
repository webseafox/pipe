using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.Telemetry.GetClientIp
{
    [ApiVersion("1")]
    [ApiConventionType(typeof(ApiConventions))]
    public class TelemetryController : ApiControllerBase
    {
        private readonly IRequestAccessor _requestAccessor;

        public TelemetryController(IRequestAccessor requestAccessor)
        {
            _requestAccessor = requestAccessor;
        }

        /// <summary>
        /// Get Client IP-Address.
        /// </summary>
        [HttpGet("client-ip")]        
        public IActionResult GetClientIp()        
        {
            try
            {
                return Ok(_requestAccessor.OriginIP);
            }
            catch (Exception ex)
            {
                return StatusCode(500, OutputBuilder.Create().WithError(ex.Message).InternalError());
            }            
        }
    }
}
