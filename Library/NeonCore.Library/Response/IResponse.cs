using System;
using System.Collections.Generic;
using System.Text;

namespace NeonCore.Library.Response
{
    public interface IResponse
    {
        String Message { get; set; }

        Boolean HasError { get; set; }

        String ErrorMessage { get; set; }
    }
}
