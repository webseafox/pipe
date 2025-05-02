using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Application.UseCases.Authenticate.ResetPassword;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.Authenticate.ResetPassword
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/authenticate")]
    [ApiConventionType(typeof(ApiConventions))]
    public class AuthenticateController : ApiControllerBase
    {
        private readonly IPresenter _presenter;
        private readonly IHttpContextAccessor _accessor;

        public AuthenticateController(IPresenter presenter, IHttpContextAccessor acessor)
        {
            _presenter = presenter;
            _accessor = acessor;
        }

        /// <summary>
        /// Reseta a senha de um usuário
        /// </summary>
        /// <param name="body">Entidade para resetar senha do usuário</param>
        /// <response code="200">Retorno dos dados do usuário</response>
        /// <response code="404">Indica que o usuário não foi encontrado</response>
        /// <response code="500">Indica um erro interno do servidor</response>
        [HttpPost]
        [Route("reset-password")]
        [ProducesResponseType(typeof(ResetPasswordOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPasswordAsync([FromHeader] string ip, [FromBody] ResetPasswordInput body)
        {
            body.SetIpAddress(ip);
            return await _presenter.Ok(body);
        }
    }
}
