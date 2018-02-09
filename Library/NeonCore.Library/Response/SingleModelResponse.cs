using System;
using System.Collections.Generic;
using System.Text;

namespace NeonCore.Library.Response
{
    public class SingleModelResponse<TModel> : ISingleModelResponse<TModel> where TModel : new()
    {
        public SingleModelResponse()
        {
            Model = new TModel();
        }

        public String Message { get; set; }

        public Boolean HasError { get; set; }

        public String ErrorMessage { get; set; }

        public TModel Model { get; set; }
    }
}
