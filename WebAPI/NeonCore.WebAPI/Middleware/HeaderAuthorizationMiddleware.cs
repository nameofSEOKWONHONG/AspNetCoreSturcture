using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NeonCore.WebAPI.Middleware
{
    public class HeaderAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HeaderAuthorizationMiddleware> _logger;

        public HeaderAuthorizationMiddleware(RequestDelegate next, ILogger<HeaderAuthorizationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogWarning($"In Use. BeforeRun, Order: {DateTime.Now}");

            //logic
            //여기서 hmac matching 진행해야 함.
            if(context.Request.Headers.TryGetValue("Authorization", out var authValue))
            {
                var split = authValue.ToString().Split(" ");
                var isPass = false;
                //임시키
                if(split[0] == "Bearer")
                {
                    // 시간설정 받아야 함.
                    //var result = GetHMACValue("", "", "");
                    //if(split[1] != result)
                    //{                        
                    //}
                }
                //고정키
                else if(split[0] == "HMAC")
                {
                    if(IsValidRequest(context, "", "", ""))
                    {

                    }
                }
                else
                {
                    Debug.WriteLine($"warning access.");
                }

                if(isPass)
                {
                    await _next.Invoke(context);
                }
            }

            await _next.Invoke(context);

            _logger.LogWarning($"In Use. AfterRun, Order:{DateTime.Now}");
        }

        bool IsValidRequest(HttpContext context, string signature, string clientIP, string curTimeStamp)
        {
            var bodystr = string.Empty;
            var req = context.Request;

            using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                bodystr = reader.ReadToEnd();
            }

            req.Body.Position = 0;

            var calcHash = GetHMACValue(clientIP, curTimeStamp, bodystr);
            return (signature.Equals(calcHash, StringComparison.Ordinal));
        }

        string GetHMACValue(string clientIp, string curTimeStamp, string data)
        {
            string message = clientIp + data + curTimeStamp;
            string key = GetHMACKey();
            System.Text.UTF8Encoding encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] hashmessage = null;

            using (HMACSHA256 hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] dataByte = encoding.GetBytes(message);
                hashmessage = hmacsha256.ComputeHash(dataByte);
            }

            return ByteToString(hashmessage);
        }

        private string ByteToString(byte[] hashmessage)
        {
            string sbinary = "";

            for(int i=0; i<hashmessage.Length; i++)
            {
                sbinary += hashmessage[i].ToString("X2");
            }

            return sbinary;
        }

        private string GetHMACKey()
        {
            return "781a4e6c6b9328aaaee873353879fdabb";
        }
    }

    public static class SampleMiddlewareExtensions
    {
        public static IApplicationBuilder UseSampleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderAuthorizationMiddleware>();
        }
    }
}
