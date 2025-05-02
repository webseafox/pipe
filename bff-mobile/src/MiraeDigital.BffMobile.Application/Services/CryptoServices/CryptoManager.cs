using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MiraeDigital.BffMobile.Application.Services.CryptoServices;

public class CryptoManager : ICryptoManager
{
    readonly CryptokeyConfig _cryptoKeyConfig;
    public CryptoManager(CryptokeyConfig cryptokeyConfig)
    {
        _cryptoKeyConfig = cryptokeyConfig;
    }

    public string Decrypt(string encryptedText)
        => Decrypt(_cryptoKeyConfig.Key, _cryptoKeyConfig.Salt, _cryptoKeyConfig.Iv, encryptedText);

    public string Encrypt(string plainText)
        => Encrypt(_cryptoKeyConfig.Key, _cryptoKeyConfig.Salt, _cryptoKeyConfig.Iv, plainText);

    public static byte[] GenerateKey(string passphrase, string saltKey)
    {
        byte[] salt = ConvertHexStringToByteArray(saltKey);
        int iterations = 1;
        var rfc2898 = new Rfc2898DeriveBytes(passphrase, salt, iterations);
        byte[] key = rfc2898.GetBytes(16);

        return key;
    }

    public static byte[] ConvertHexStringToByteArray(string hexString)
    {
        int NumberChars = hexString.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        return bytes;
    }

    public static string Encrypt(string key, string salt, string ivKey, string plainText)
    {
        try
        {
            byte[] keyBytes = GenerateKey(key, salt);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                var iv = ConvertHexStringToByteArray(ivKey);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv);

                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                return Convert.ToBase64String(encryptedBytes);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error encrypting text.", ex);
        }
    }

    public static string Decrypt(string key, string salt, string ivKey, string encryptedText)
    {
        try
        {
            byte[] keyBytes = GenerateKey(key, salt);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                var iv = ConvertHexStringToByteArray(ivKey);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv);

                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error decrypting ciphered text.", ex);
        }        
    }
}
