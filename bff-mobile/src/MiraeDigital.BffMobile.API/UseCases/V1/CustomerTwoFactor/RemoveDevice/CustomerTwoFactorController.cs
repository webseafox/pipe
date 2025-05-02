using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RemoveDevice;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.CustomerTwoFactor.RemoveDevice
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
        /// Removes the current device from the account.
        /// </summary>
        /// <param name="input">Customer and device data.</param>        
        [Authorize(Policy = Policies.RequireLOA3)]
        [HttpDelete]
        [Route("device/remove")]
        [ProducesResponseType(typeof(RemoveDeviceOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> RegisterDevice([FromBody] RemoveDeviceInput input)
        {
            return await _presenter.Ok(input);
        }
    }
}
