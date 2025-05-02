using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;
using MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RegisterDevice;
using Microsoft.Net.Http.Headers;
using MiraeDigital.BffMobile.Domain.Dtos.Token;

namespace MiraeDigital.BffMobile.API.UseCases.V1.CustomerTwoFactor.RegisterDevice
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

        [Authorize(Policy = Policies.RequireLOA2)]
        [HttpPost]
        [Route("device/registration")]
        [ProducesResponseType(typeof(RegisterDeviceOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> RegisterDevice([FromBody] RegisterDeviceInput input)
        {            
            return await _presenter.Ok(input);
        }
    }
}
