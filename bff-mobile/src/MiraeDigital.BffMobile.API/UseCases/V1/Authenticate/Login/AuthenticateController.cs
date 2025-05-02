using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Application.UseCases.Authenticate.Login;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.Authenticate.Login
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
        /// Authenticate user by document and password.
        /// </summary>
        /// <param name="body">Login data</param>
        /// <response code="200">Authentication data</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal error.</response>
        [HttpPost]        
        [ProducesResponseType(typeof(LoginOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginInput body)        
        {            
            return await _presenter.Ok(body);
        }
    }
}
