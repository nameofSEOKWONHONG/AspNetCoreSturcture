using Newtonsoft.Json;
using System.Collections.Generic;

public static class JsonExtension
{
    public static string SetJson<T>(this IEnumerable<T> source)
    {
        return JsonConvert.SerializeObject(source);
    }

    public static T GetJson<T>(this string value)
    {
        if (string.IsNullOrEmpty(value)) return default(T);

        return JsonConvert.DeserializeObject<T>(value);
    }
}
