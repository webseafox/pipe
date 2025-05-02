using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Application.UseCases.Authenticate.ValidateElectronicSignature;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.Authenticate.ValidateElectronicSignature
{
    [ApiVersion("1")]
    [ApiConventionType(typeof(ApiConventions))]
    public class AuthenticateController : ApiControllerBase
    {
        private readonly IPresenter _presenter;

        public AuthenticateController(IPresenter presenter)
        {
            _presenter = presenter;
        }

        /// <summary>
        /// Validate user's electronic signature.
        /// </summary>        
        /// <param name="input">Electronic signature</param>
        /// <response code="200">Validation result</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal error.</response>
        [HttpPost]
        [Route("electronicsignature/validate")]
        [Authorize(Policy = Policies.RequireLOA2)]
        [ProducesResponseType(typeof(ValidateElectronicSignatureOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ValidateElectronicSignatureAsync([FromBody] ValidateElectronicSignatureInput input)
        {
            return await _presenter.Ok(input);
        }
    }
}
