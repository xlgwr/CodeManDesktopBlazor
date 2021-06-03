using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace System
{
    public static class ObjectExt
    {

        
        public static JsonSerializerOptions optionSerial = new JsonSerializerOptions
        {
            AllowTrailingCommas = true
        };
        public static T JsonTo<T>(this string m)
        {
            if (m == null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(m, optionSerial);
        }
        public static object JsonTo(this string value, Type type)
        {
            return JsonSerializer.Deserialize(value, type, optionSerial);
        }
        public static string toJsonStr(this object m)
        {
            if (m == null)
            {
                return "";
            }
            return JsonSerializer.Serialize<object>(m, optionSerial);
        }
        public static string ToNotNullString(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.ToString();
        }
    }
}
