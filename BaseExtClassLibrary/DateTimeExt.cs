using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace System
{ 
    public static class DateTimeExt
    {
        /// <summary>
        /// 判断是否为工作日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsWorkDay(this DateTime dt)
        {
            //先从日期表中，查找不是上班时间，如果不是直接返回 false ，如果是，直接返回 true。
            //如果在日期表中，找不到，则查找定义的日历，依据日历定义的周末时间来定义是否为工作日。
            //获取日历中不上班的标准周末时间,判断是不是上班时间
            if (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday)
                return false;
            else
                return true;
        }
        public static string ToyyyyMMddExt(this DateTime obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString("yyyyMMdd");
        }

        public static string ToyyyyMMddExt(this DateTime? obj)
        {
            if (obj == null)
                return string.Empty;
            else
            {
                DateTime dt = (DateTime)obj;
                return dt.ToString("yyyyMMdd");
            }
        }

        public static string ToHHmmssExt(this DateTime obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString("HHmmss");
        }

        public static string ToHHmmssfffExt(this DateTime obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString("HHmmssfff");
        }

        public static string ToyyyyMMddHHmmssExt(this DateTime obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString("yyyyMMddHHmmss");
        }

        public static DateTime? TryParseExt(this string dateStr)
        {
            DateTime dateTime = DateTime.Now;
            if (DateTime.TryParse(dateStr, out dateTime))
            {
                return dateTime;
            }
            return null;
        }

        /// <summary>
        /// 时分秒时间补零  传入数据为93000等格式,即为9点30，返回093000
        /// </summary>
        /// <param name="str"></param>
        /// <returns>返回补零后的字符串</returns>
        public static string TimeStrAddZeroExt(this string str)
        {
            if (str != null || str.Length < 6)
            {
                StringBuilder sb = new StringBuilder();
                var length = str.Length;
                for (int i = length; i < 6; i++)
                {
                    sb.Append("0");
                }

                return $"{sb.ToString()}{str}";
            }
            return str;
        }
        public static string get_uft82(this string unicodeString)
        {
            if (unicodeString.isNull())
            {
                return "";
            }
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes);
            return decodedString;
        }
        /// <summary>
        /// 以UTF-8带BOM格式返回utf8编码
        /// </summary>
        /// <param name="unicodeString"></param>
        /// <returns></returns>
        public static string get_uft8(this string text)
        {

            if (text.isNull())
            {
                return "";
            }
            //声明字符集   
            System.Text.Encoding utf8, utf8bom;

            //Default   //以UTF-8带BOM格式读取文件内容
            utf8bom = new UTF8Encoding(true);
            //utf8   
            utf8 = new UTF8Encoding(false);
            byte[] gb;
            gb = utf8bom.GetBytes(text);
            gb = System.Text.Encoding.Convert(utf8bom, utf8, gb);
            //返回转换后的字符   
            var result = utf8.GetString(gb);

            //using (var sink = new StreamWriter("FoobarNoBom.txt", false, utf8WithoutBom))
            //{
            //    sink.WriteLine(result);
            //}
            //using (var sink = new StreamWriter("FoobarBom.txt", false, utf8bom))
            //{
            //    sink.WriteLine(result);
            //}

            return result;
        }
        /// <summary>
        /// GB2312转换成UTF8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string gb2312_utf8(this string text)
        {
            if (text.isNull())
            {
                return "";
            }
            //声明字符集   
            System.Text.Encoding utf8, gb2312;

            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
            //返回转换后的字符   
            return utf8.GetString(gb);
        }

        /// <summary>
        /// UTF8转换成GB2312
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string utf8_gb2312(this string text)
        {
            if (text.isNull())
            {
                return "";
            }
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] utf;
            utf = utf8.GetBytes(text);
            utf = System.Text.Encoding.Convert(utf8, gb2312, utf);
            //返回转换后的字符   
            return gb2312.GetString(utf);
        }

        /// <summary>
        /// 将自定义对象序列化为XML字符串
        /// </summary>
        /// <param name="myObject">自定义对象实体</param>
        /// <returns>序列化后的XML字符串</returns>
        public static string SerializeToXml<T>(this T myObject)
        {
            if (myObject != null)
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));

                MemoryStream stream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
                writer.Formatting = System.Xml.Formatting.None;//缩进
                xs.Serialize(writer, myObject);

                stream.Position = 0;
                StringBuilder sb = new StringBuilder();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                    reader.Close();
                }
                writer.Close();
                return sb.ToString();
            }
            return string.Empty;
        }

        //xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"

        public static string SerializeToXmlWithNoHead<T>(this T myObject)
        {
            if (myObject != null)
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));

                MemoryStream stream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
                writer.Formatting = System.Xml.Formatting.None;//缩进
                xs.Serialize(writer, myObject);

                stream.Position = 0;
                StringBuilder sb = new StringBuilder();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                    reader.Close();
                }
                writer.Close();
                return sb.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf - 8\"?>", "").Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
            }
            return string.Empty;
        }

        /// <summary>
        /// 将XML字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xml">XML字符</param>
        /// <returns></returns>
        public static T DeserializeToObject<T>(this string xml)
        {
            T myObject;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xml);
            myObject = (T)serializer.Deserialize(reader);
            reader.Close();
            return myObject;
        }
        /// <summary>
        /// HH:mm
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static bool NowIsInSpanHM(this string timeSpan)
        {
            var timeSpans = timeSpan.Split('-');
            var now = DateTime.Now.ToString("HH:mm").Replace(":", "").ToInt();
            var start = timeSpans[0].Replace(":", "").ToInt();
            var end = timeSpans[1].Replace(":", "").ToInt();
            if (start <= end)
            {
                if (now >= start && now <= end)
                {
                    return true;
                }
            }
            else
            {
                if (now >= start || now <= end)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// HH:mm:ss
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static bool NowIsInSpanHMS(this string timeSpan)
        {

            var timeSpans = timeSpan.Split('-');
            var now = DateTime.Now.ToString("HH:mm:ss").Replace(":", "").ToInt();
            var start = timeSpans[0].Replace(":", "").ToInt();
            var end = timeSpans[1].Replace(":", "").ToInt();
            if (start <= end)
            {
                if (now >= start && now <= end)
                {
                    return true;
                }
            }
            else
            {
                if (now >= start || now <= end)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsInTimeSpan(this string timeSpan, int time)
        {
            var timeSpans = timeSpan.Split('-');
            var now = time;
            var start = timeSpans[0].Replace(":", "").ToInt();
            var end = timeSpans[1].Replace(":", "").ToInt();
            if (start <= end)
            {
                if (now >= start && now <= end)
                {
                    return true;
                }
            }
            else
            {
                if (now >= start || now <= end)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
