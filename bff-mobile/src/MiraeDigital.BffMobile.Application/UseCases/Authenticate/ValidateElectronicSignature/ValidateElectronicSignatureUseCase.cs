using MiraeDigital.Lib.Application.UseCases;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.UseCases.Authenticate.ValidateElectronicSignature
{
    public sealed class ValidateElectronicSignatureUseCase : IUseCase<ValidateElectronicSignatureInput>
    {
        public Task<Output> Handle(ValidateElectronicSignatureInput request, CancellationToken cancellationToken)
        {           
            return Task.FromResult(OutputBuilder.Create().WithResult(new ValidateElectronicSignatureOutput()).Response());
        }
    }
}
