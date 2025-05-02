using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Application.UseCases.CheckingAccount.GetCheckingAccountBalance;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.CheckingAccount.GetCheckingAccountBalance
{
    [ApiVersion("1")]
    [ApiConventionType(typeof(ApiConventions))]
    public class UserController : ApiControllerBase
    {
        private readonly IPresenter _presenter;

        public UserController(IPresenter presenter)
        {
            _presenter = presenter;
        }

        /// <summary>
        /// Realiza a busca do saldo de um cliente específico.
        /// </summary>
        /// <response code="200">Retorno dos dados do saldo</response>
        /// <response code="404">Indica que o saldo não foi encontrado com o determinado código cliente</response>
        /// <response code="500">Indica um erro interno do servidor</response>
        [HttpGet]
        [Authorize(Policy = Policies.RequireLOA3)]
        [Route("account/balance")]
        [ProducesResponseType(typeof(GetCheckingAccountBalanceOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCheckingAccountBalanceAsync()
        {                 
            return await _presenter.Ok(new GetCheckingAccountBalanceInput());
        }
    }
}

