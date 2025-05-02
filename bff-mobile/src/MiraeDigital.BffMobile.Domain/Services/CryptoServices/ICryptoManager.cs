using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Services.CryptoServices
{
    public interface ICryptoManager
    {
        string Decrypt(string encryptedText);
        string Encrypt(string plainText);
    }
}
