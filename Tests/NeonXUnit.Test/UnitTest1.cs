using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neon.Payment;
using Neon.Payment.BusinessLayer;
using Neon.Payment.BusinessLayer.Contract;
using NeonCore.Library;
using System;
using Xunit;

namespace NeonXUnit.Test
{
    public class UnitTest1
    {
        IOptions<AppSettings> options;
        PaymentContext dbcontext;

        public UnitTest1()
        {
            options = Options.Create<AppSettings>(new AppSettings()
            {
                PaymentInstance = "Server=(localdb)\\mssqllocaldb;Database=Payment;Trusted_Connection=True;integrated security=yes;"
            });
            dbcontext = new PaymentContext(options);
        }

        [Fact]
        public void GetOrdersTest()
        {
            IOrderBusinessObject obj = new OrderBusinessObject(null, dbcontext);

            Assert.NotNull(obj);
        }

        [Fact]
        public void UnitTest2()
        {
            var curTimeStamp = GetTimeStamp();
            var clientIP = "127.0.0.1";
            var bearerAuth = 
        }

        public string GetTimeStamp()
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var now = DateTime.Now;
            TimeSpan elapsedTime = now - epoch;
            var time = (long)elapsedTime.TotalSeconds;
            return Convert.ToString(time);
        }
    }
}
