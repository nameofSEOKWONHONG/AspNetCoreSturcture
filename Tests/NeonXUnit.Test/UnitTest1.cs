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
    }
}
