using System;
using System.Collections.Generic;
using System.Text;

namespace NeonCore.Library
{
    public class UserInfo : IUserInfo
    {
        public string Domain { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }
}
