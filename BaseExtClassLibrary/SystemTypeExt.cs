using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class SystemTypeExt
    {
        
        public static byte ToByte(this sbyte value)
        {

            return Convert.ToByte(value);
        }
        public static byte ToByte(this char value)
        {

            return Convert.ToByte(value);
        }

        public static sbyte ToSByte(this byte value)
        {

            return Convert.ToSByte(value);
        }
        public static char ToChar(this byte value)
        {

            return Convert.ToChar(value);
        }
        public static string TryToString(this object obj)
        {
            if (obj == null)
                return string.Empty;
            else
                return obj.ToString();
        }
        public static int TryToInt(this object obj)
        {
            int tmp = default(int);
            if (obj == null)
                return tmp;
            else
            {
                int.TryParse(obj.TryToString(), out tmp);
            }
            return tmp;
        }

        public static decimal? TryToDecimalOrNull(this object obj)
        {
            if (obj == null)
                return null;
            else
            {
                decimal tmp;
                if (decimal.TryParse(obj.TryToString(), out tmp))
                    return tmp;
                else
                    return null;
            }
        }
        public static decimal TryToDecimal(this object obj, decimal defaultvalue = 0)
        {
            if (obj == null)
                return defaultvalue;
            else
            {
                decimal tmp;
                if (decimal.TryParse(obj.TryToString(), out tmp))
                    return tmp;
                else
                    return defaultvalue;
            }
        }

        public static sbyte TryToSByte(this object obj)
        {
            if (obj == null)
                return default(sbyte);
            else
            {
                sbyte tmp;
                sbyte.TryParse(obj.TryToString(), out tmp);
                return tmp;
            }
        }
        public static byte TryToByte(this object obj)
        {
            if (obj == null)
                return default(byte);
            else
            {
                byte tmp;
                byte.TryParse(obj.TryToString(), out tmp);
                return tmp;
            }
        }
        public static DateTime? TryToDetaTime(this object obj)
        {
            if (obj == null)
                return null;
            else
            {
                try
                {
                    DateTime tmp;
                    tmp = DateTime.ParseExact(obj.TryToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    return tmp;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static string BoolToIntString(this bool b)
        {
            if (b)
            {
                return "1";
            }
            return "0";

        }
        public static string BoolToIntString(this bool? b)
        {
            if (b == true)
            {
                return "1";
            }
            return "0";

        }
    }
}
