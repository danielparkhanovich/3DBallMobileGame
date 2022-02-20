using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

public enum DataType
{
    DIAMONDSCOUNT,
    HIGHSCORECOUNT
}

public static class DataCipher
{
    private readonly static string cipherKey = "A60A5770FE5E7AB200BA9CFC94E4E8B0"; // Any string of 32 chars
    private readonly static string iv = "1234567887654321"; // Any string of 16 chars

    private static string AESEncryption(string inputData)
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(cipherKey);
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = ASCIIEncoding.ASCII.GetBytes(inputData);
        ICryptoTransform trnsfrm = AEScryptoProvider.CreateEncryptor(AEScryptoProvider.Key, AEScryptoProvider.IV);

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return Convert.ToBase64String(result);
    }

    private static string AESDecryption(string inputData)
    {
        AesCryptoServiceProvider AEScryptoProvider = new AesCryptoServiceProvider();
        AEScryptoProvider.BlockSize = 128;
        AEScryptoProvider.KeySize = 256;
        AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(cipherKey);
        AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes(iv);
        AEScryptoProvider.Mode = CipherMode.CBC;
        AEScryptoProvider.Padding = PaddingMode.PKCS7;

        byte[] txtByteData = Convert.FromBase64String(inputData);
        ICryptoTransform trnsfrm = AEScryptoProvider.CreateDecryptor();

        byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
        return ASCIIEncoding.ASCII.GetString(result);
    }

    public static void SaveElement(string key, string data)
    {
        var encrypted = AESEncryption(data);
        PlayerPrefs.SetString(key, encrypted);
    }

    public static string LoadElement(string key)
    {
        var loadedData = PlayerPrefs.GetString(key);
        return AESDecryption(loadedData);
    }
}
