using MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer;
using MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomerAccounts;
using Refit;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Http.Registration
{
    public interface IRegistrationClient
    {
        [Get("/api/v1/Customer/accounts/{document}")]
        Task<GetCustomerAccountsResponse> GetCustomerAccountsAsync([AliasAs("document")] long document);

        [Get("/api/v1/Customer/{customerId}")]        
        Task<GetCustomerResponse> GetCustomerByIdAsync([AliasAs("customerId")] long customerId);
    }
}
