using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NeonCore.WebAPI.Middleware
{
    public class AES256Cipher
    {
        string logKey = "";
        CipherInfo _cipherInfo;

        public AES256Cipher()
        {
            var hmac = new HMACCipher();
            _cipherInfo = hmac.GetCipherInfo();            
        }

        public string EncodeAES256(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                byte[] byteKey = Encoding.UTF8.GetBytes(_cipherInfo.Key);
                byte[] byteIV = Encoding.UTF8.GetBytes(_cipherInfo.InitialVector);
                var encText = string.Empty;
                byte[] bytePlainText = Encoding.UTF8.GetBytes(plainText);

                using(AesCryptoServiceProvider aesCrytoProvider = new AesCryptoServiceProvider())
                {
                    try
                    {
                        aesCrytoProvider.Mode = _cipherInfo.Mode;
                        aesCrytoProvider.Padding = _cipherInfo.PaddingMode;
                        return GetConvertCipherText(aesCrytoProvider.CreateEncryptor(byteKey, byteIV).TransformFinalBlock(bytePlainText, 0, bytePlainText.Length));
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
            }

            return string.Empty;
        }

        string GetConvertCipherText(byte[] cipherBytes)
        {
            switch(_cipherInfo.OutputFormat)
            {
                case "HEX":
                    return ByteToHexString(cipherBytes);
                case "BASE64":
                    return Convert.ToBase64String(cipherBytes);
            }

            return null;
        }

        public static string ByteToHexString(byte[] buff)
        {
            string sbinary = "";

            for(int i=0; i<buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2");
            }

            return sbinary;
        }

        public static byte[] HexStringToByte(string hexString)
        {
            try
            {
                var bytes = new byte[hexString.Length / 2];
                for(var i=0; i<bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                }

                return bytes;
            }
            catch
            {
                return null;
            }
        }
    }
}
