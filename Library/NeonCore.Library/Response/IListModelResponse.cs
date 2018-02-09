using System;
using System.Collections.Generic;
using System.Text;

namespace NeonCore.Library.Response
{
    public interface IListModelResponse<TModel> : IResponse
    {
        Int32 PageSize { get; set; }

        Int32 PageNumber { get; set; }

        Int32 TotalSize { get; set; }

        IEnumerable<TModel> Model { get; set; }
    }
}
