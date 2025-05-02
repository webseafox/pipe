using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.SendToken;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.CustomerTwoFactor.SendToken
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/customer-two-factor")]
    [ApiConventionType(typeof(ApiConventions))]
    public class CustomerTwoFactorController : ApiControllerBase
    {
        private readonly IPresenter _presenter;

        public CustomerTwoFactorController(IPresenter presenter)
        {
            _presenter = presenter;
        }

        /// <summary>
        /// Sends a token to validate customer action.
        /// </summary>
        /// <param name="body">SendToken data</param>
        /// <response code="200">Delivery information.</response>
        /// <response code="500">Internal error.</response>
        [HttpPost]
        [Route("token/send")]
        [Authorize(Policy = Policies.RequireLOA1)]
        [ProducesResponseType(typeof(SendTokenOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendTokenAsync([FromBody] SendTokenInput body)
        {
            return await _presenter.Ok(body);
        }
    }
}
