using MiraeDigital.BffMobile.Domain.Services.ElectronicSignature;
using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.Authenticate.ValidateElectronicSignature
{
    public sealed class ValidateElectronicSignatureInput : IElectronicSignatureInput, IUseCaseInput
    {
        public string ESignature { get; set; }
    }
}
