using System;
using System.Collections.Generic;
using System.Text;

namespace NeonCore.Library.Response
{
    public interface ISingleModelResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }
}
