using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Application.UseCases.Statements.GetAllStatementsDateGroup;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.Statements.GetAllStatementsDateGroup
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/account")]
    [ApiConventionType(typeof(ApiConventions))]
    public class StatementController : ApiControllerBase
    {
        private readonly IPresenter _presenter;

        public StatementController(IPresenter presenter)
        {
            _presenter = presenter;
        }

        /// <summary>
        /// Realiza a consulta do extrato do cliente agrupado pela data.
        /// </summary>
        /// <param name="filter">Filtro do extrato</param>
        [HttpGet]
        [Authorize(Policy = Policies.RequireLOA3)]
        [Route("statement")]
        [ProducesResponseType(typeof(GetAllStatementsDateGroupOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStatementsDateGroupAsync([FromQuery] GetAllStatementsDateGroupInput filter)
        {
            return await _presenter.Ok(filter);
        }
    }
}
