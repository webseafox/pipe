using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Application.UseCases.Account.GetBasicInformation;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.Account.GetBasicInformation
{
    [ApiVersion("1")]
    [ApiConventionType(typeof(ApiConventions))]
    public class AccountController : ApiControllerBase
    {
        private readonly IPresenter _presenter;

        public AccountController(IPresenter presenter)
        {
            _presenter = presenter;
        }

        /// <summary>
        /// Get account basic information by token.
        /// </summary>
        /// <response code="200">Return basic information data.</response>
        /// <response code="400">Error while getting customer information.</response>
        /// <response code="500">Internal error.</response>
        [HttpGet]
        [Authorize(Policy = Policies.RequireLOA3)]
        [Route("basic/information")]
        [ProducesResponseType(typeof(GetBasicInformationOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBasicInformationByToken()
        {
            return await _presenter.Ok(new GetBasicInformationInput());
        }
                
    }
        
}
