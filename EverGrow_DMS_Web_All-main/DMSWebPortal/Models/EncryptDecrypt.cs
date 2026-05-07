
using System.Security.Cryptography;
using System.Text;

public class EncryptDecrypt
{
    private readonly string prm_key = "lkirwf897+22#bbtrm8814z5qq=498j5";
    private readonly string prm_iv = "741952hheeyy66#cs!9hjv887mxx7@8y";
    private byte[] GetKey() => Encoding.UTF8.GetBytes(prm_key.Substring(0, 32));
    private byte[] GetIV() => Encoding.UTF8.GetBytes(prm_iv.Substring(0, 16));

    public string Encrypt(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
            return "";

        using (Aes aes = Aes.Create())
        {
            aes.Key = GetKey();
            aes.IV = GetIV();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
                sw.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrWhiteSpace(cipherText))
            return "";

        using (Aes aes = Aes.Create())
        {
            aes.Key = GetKey();
            aes.IV = GetIV();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] buffer = Convert.FromBase64String(cipherText);

            using (var ms = new MemoryStream(buffer))
            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}