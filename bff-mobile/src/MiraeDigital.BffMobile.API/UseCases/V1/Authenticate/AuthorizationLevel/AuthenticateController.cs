using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Application.UseCases.Authenticate.AuthorizationLevel;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.Authenticate.AuthorizationLevel
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
        /// Increase authentication level with MFA Token        
        /// </summary>
        /// <remarks>
        /// MFA token method values: "email", "app".
        /// </remarks>
        /// <param name="input">Authorization level data</param>
        [HttpPost("authorization")]
        [Authorize(Policy = Policies.RequireLOA1)]
        [ProducesResponseType(typeof(AuthorizationLevelOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthorizationLevelAsync([FromBody] AuthorizationLevelInput input)
        {                        
            return await _presenter.Ok(input);
        }
    }
}
