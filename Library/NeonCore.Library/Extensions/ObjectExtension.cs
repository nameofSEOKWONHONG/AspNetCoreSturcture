using System;
using System.Collections.Generic;
using System.Text;

public static class ObjectExtension
{
    public static bool IsExist(this object obj)
    {
        if(obj != null)
        {
            return true;
        }

        return false;
    }
}
