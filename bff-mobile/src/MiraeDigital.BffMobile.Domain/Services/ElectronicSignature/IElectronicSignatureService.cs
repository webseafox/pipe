using MiraeDigital.BffMobile.Domain.Enums;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Services.ElectronicSignature;

public interface IElectronicSignatureService
{
    Task<SignatureResult> Validate(string eSignature, string ipAddress, string token);
}
