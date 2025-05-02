using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Application.UseCases.Positions.GetConsolidatedPositionByClientCode;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.Positions.GetConsolidatedPositionByClientCode
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/financial")]
    [ApiConventionType(typeof(ApiConventions))]
    public class PositionController : ApiControllerBase
    {
        private readonly IPresenter _presenter;

        public PositionController(IPresenter presenter)
        {
            _presenter = presenter;
        }

        /// <summary>
        /// Realiza a consulta da posição de bmf do cliente.
        /// </summary>
        [HttpGet]
        [Authorize(Policy = Policies.RequireLOA3)]
        [Route("position/invested")]
        [ProducesResponseType(typeof(GetConsolidatedPositionByClientCodeOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPagedPositionAsync()
        {
            return await _presenter.Ok(new GetConsolidatedPositionByClientCodeInput());
        }
    }
}
