using System;
using System.Collections.Generic;
using System.Text;

namespace NeonCore.Library.Response
{
    public class ListModelResponse<TModel> : IListModelResponse<TModel>
    {
        public String Message { get; set; }

        public Boolean HasError { get; set; }

        public String ErrorMessage { get; set; }

        public Int32 PageSize { get; set; }

        public Int32 PageNumber { get; set; }

        public Int32 TotalSize { get; set; }

        public IEnumerable<TModel> Model { get; set; }
    }
}
