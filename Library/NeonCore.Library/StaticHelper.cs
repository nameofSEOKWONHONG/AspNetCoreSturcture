using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

public static class StaticHelper
{
    public static string ToGetParam<T>(this T obj)
    {
        var parameter = string.Empty;

        var propertyInfos = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.MemberType == MemberTypes.Field || x.MemberType == MemberTypes.Property);

        foreach (var propertyInfo in propertyInfos)
        {
            parameter += propertyInfo.Name + "=" + propertyInfo.GetValue(obj) + "&";
        }

        return parameter.Substring(0, parameter.Length - 1);
    }

    public static string ToJson<T>(this T obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T ToEntity<T>(this string obj)
    {
        return JsonConvert.DeserializeObject<T>(obj);
    }
}
