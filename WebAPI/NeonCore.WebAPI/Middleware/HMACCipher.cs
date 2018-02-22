using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NeonCore.WebAPI.Middleware
{
    public class HMACCipher
    {
        public CipherInfo GetCipherInfo()
        {
            return new CipherInfo
            {
                Key = "enc_key",
                InitialVector = "000000000000000"
            };
        }
    }

    public class CipherInfo
    {
        public string Key { get; set; }
        public string InitialVector { get; set; }
        public CipherMode Mode { get; set; } = CipherMode.ECB;
        public PaddingMode PaddingMode { get; set; } = PaddingMode.PKCS7;
        public string OutputFormat { get; set; } = "HEX";
    }
}
