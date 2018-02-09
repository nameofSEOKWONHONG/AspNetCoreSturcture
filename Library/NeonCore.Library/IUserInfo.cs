using System;
using System.Collections.Generic;
using System.Text;

namespace NeonCore.Library
{
    public interface IUserInfo
    {
        string Domain { get; set; }
        string Name { get; set; }
        string[] Roles { get; set; }
    }
}
