using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace NeonCore.WebAPI.Controllers
{
    public class BaseController : Controller
    {
        protected ILogger Logger { get; }
        protected ILogger DbLogger { get; }

        public BaseController()
        {
            Logger = LogManager.GetLogger(GetType().FullName);
            DbLogger = LogManager.GetLogger("database");
        }
    }
}