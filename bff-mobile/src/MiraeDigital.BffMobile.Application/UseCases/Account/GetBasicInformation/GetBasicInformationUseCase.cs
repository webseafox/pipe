using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Registration;
using MiraeDigital.BffMobile.Domain.Http;
using MiraeDigital.Lib.Application.UseCases;
using System.Threading;
using System.Threading.Tasks;
using MiraeDigital.BffMobile.Domain.Http.Suitability;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GetUser;
using MiraeDigital.BffMobile.Domain.SeedWork;

namespace MiraeDigital.BffMobile.Application.UseCases.Account.GetBasicInformation
{
    public class GetBasicInformationUseCase : IUseCase<GetBasicInformationInput>
    {
        private readonly IRequestAccessor _requestAccessor;
        private readonly IRefitHttpHandler _refitHttpHandler;        
        private readonly IRegistrationClient _registrationClient;
        private readonly ISuitabilityClient _suitabilityClient;
        private readonly IAuthenticationClient _authenticationClient;

        public GetBasicInformationUseCase(IRefitHttpHandler refitHttpHandler, IRegistrationClient registrationClient, ISuitabilityClient suitabilityClient, IAuthenticationClient authenticationClient, IRequestAccessor requestAccessor)
        {
            _refitHttpHandler = refitHttpHandler;
            _registrationClient = registrationClient;
            _suitabilityClient = suitabilityClient;
            _authenticationClient = authenticationClient;
            _requestAccessor = requestAccessor;
        }

        public async Task<Output> Handle(GetBasicInformationInput request, CancellationToken cancellationToken)
        {
            try
            {
                OutputBuilder outputBuilder = OutputBuilder.Create();

                var customerResult = await _refitHttpHandler.Execute(async () => await _registrationClient.GetCustomerByIdAsync(_requestAccessor.User.CustomerID));
                if (customerResult.HasError()) return outputBuilder.WithError("Registration returned error.").BadRequestError();

                var usersResult = await _refitHttpHandler.Execute(async () => await _authenticationClient.GetUsersByDocumentAsync(_requestAccessor.User.Document));
                if (usersResult.HasError()) return outputBuilder.WithError("Authentication returned error when get user information.").BadRequestError();

                var suitabilityResult = await _refitHttpHandler.Execute(async () => await _suitabilityClient.GetCurrentByDocumentAsync(_requestAccessor.User.Document));               
                if (suitabilityResult.HasError()) return outputBuilder.WithError("Suitability returned error.").BadRequestError();

                UserResponse userResponse = usersResult.ResultData.GetUser(_requestAccessor.User.UserID);
                GetBasicInformationOutput basicInfoOutput = GetBasicInformationOutput.Create(customerResult.ResultData, suitabilityResult.ResultData, userResponse);
                return outputBuilder.WithResult(basicInfoOutput).Response();
            }
            catch (System.Exception ex)
            {
                return OutputBuilder.Create().WithError(ex.Message).InternalError();
            }
        }
    }
}
