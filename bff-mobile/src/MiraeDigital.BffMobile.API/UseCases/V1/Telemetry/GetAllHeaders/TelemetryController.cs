using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;
using System.Reflection.PortableExecutable;

namespace MiraeDigital.BffMobile.API.UseCases.V1.Telemetry.GetAllHeaders
{
    [ApiVersion("1")]
    [ApiConventionType(typeof(ApiConventions))]
    public class TelemetryController : ApiControllerBase
    {
        private readonly IRequestAccessor _requestAccessor;
        private readonly IHttpContextAccessor _accessor;


        public TelemetryController(IRequestAccessor requestAccessor, IHttpContextAccessor accessor)
        {
            _requestAccessor = requestAccessor;
            _accessor = accessor;
        }

        /// <summary>
        /// Get all headers.
        /// </summary>
        [HttpGet("all-headers")]        
        public IActionResult GetAllHeaders()
        {
            var headers = _accessor.HttpContext.Request.Headers.ToDictionary(s => s.Key, s => s.Value);

            var RemoteIpAddress = _accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            

            return Ok(new { headers, RemoteIpAddress });
        }
    }
}
