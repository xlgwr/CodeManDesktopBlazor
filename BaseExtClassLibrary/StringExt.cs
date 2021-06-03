using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringExt
    {

        public static string NullToStr(this object m, string defaultvalue = "", char trimChar = '、')
        {
            if (m == null)
            {
                return defaultvalue;
            }
            return m.ToString().Trim(trimChar);
        }
        public static bool IsNotNullOrWhiteSpace(this string str)
        {

            return !string.IsNullOrWhiteSpace(str);

        }
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        #region 字符串
        public static decimal ToDecimal(this string str)
        {
            if (str.IsNullOrWhiteSpace())
            {
                return 0;
            }
            return decimal.Parse(str);
        }
        public static double ToDouble(this string m, double defaultV = 0)
        {
            double result = 0.00;
            if (double.TryParse(m, out result))
            {
                return result;
            };
            return defaultV;
        }
        public static int ToInt(this string m, int defaultV = 0)
        {
            int result = 0;
            if (int.TryParse(m, out result))
            {
                return result;
            };
            return defaultV;
        }
        public static int TryToInt(this string str)
        {
            int result = default;
            if (string.IsNullOrWhiteSpace(str))
            {
                return result;
            }

            int.TryParse(str, out result);
            return result;
        }
        public static decimal StringToDecimal(this string str)
        {
            return decimal.Parse(str);
        }
        public static int StringToInt(this string str)
        {
            return int.Parse(str);
        }
        public static bool Contains(this string m, List<string> checkV)
        {
            if (m.isNull() || checkV == null)
            {
                return false;
            }
            foreach (var item in checkV)
            {
                if (m.ContainsWithLower(item))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 字符串
        /// </summary>
        /// <param name="m"></param>
        /// <param name="checkV"></param>
        /// <returns></returns>
        public static bool ContainsWithLower(this string m, string checkV)
        {
            if (m.isNull() || checkV.isNull())
            {
                return false;
            }
            if (m.ToLower().Contains(checkV.ToLower()))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 文件路径获取内容，（文件）
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string FilePathGetContent(this string m, Encoding encoder)
        {
            string currcontent = string.Empty;
            try
            {
                if (!File.Exists(m))
                {
                    return currcontent;
                }
                using (FileStream fs = File.Open(m, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fs, encoder))
                {
                    currcontent = sr.ReadToEnd();
                }
            }
            catch //(Exception ex)
            {
                return currcontent;
            }
            return currcontent;
        }
        /// <summary>
        /// 文件路径获取内容，（文件）
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string[] FilePathGetAllLine(this string m, int SkipLine, Encoding encoder)
        {
            var currcontent = new string[0];
            try
            {
                if (!File.Exists(m))
                {
                    return currcontent;
                }
                var listAll = new List<string>();
                int i = 0;
                using (FileStream fs = new FileStream(m, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fs, encoder))
                {
                    while (sr.Peek() > -1)
                    {
                        i++;
                        var currLine = sr.ReadLine();
                        if (i <= SkipLine)
                        {
                            continue;
                        }
                        listAll.Add(currLine);

                    }
                }
                return listAll.ToArray();
            }
            catch //(Exception ex)
            {
                return currcontent;
            }
        }
        /// <summary>
        /// 文件路径获取内容，（文件）
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static IEnumerable<string> FilePathReadAllLines(this string m, int skipRow, Encoding encoder)
        {
            IEnumerable<string> currcontent = new List<string>();
            try
            {
                if (!File.Exists(m))
                {
                    return currcontent;
                }
                currcontent = File.ReadAllLines(m, encoder).Skip(skipRow);

                return currcontent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool FilePathSaveContent(this string m, string content, Encoding encoder)
        {
            try
            {
                File.WriteAllText(m, content, encoder);
            }
            catch //(Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool FileExists(this string m)
        {
            try
            {
                if (m.isNull())
                {
                    return false;
                }
                return File.Exists(m);
            }
            catch //(Exception ex)
            {
                return false;
            }
        }
        public static bool FileDelete(this string m)
        {
            try
            {
                if (m.isNull())
                {
                    return false;
                }
                if (m.FileExists())
                {
                    File.Delete(m);
                    return true;
                }
            }
            catch //(Exception ex)
            {
                return false;
            }
            return false;
        }
        ///   <summary>
        ///   给一个字符串进行MD5加密
        ///   </summary>
        ///   <param   name="strText">待加密字符串</param>
        ///   <returns>加密后的字符串</returns>
        public static string MD5Encrypt(this string strText)
        {
            if (strText.isNull())
            {
                return strText;
            }
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(strText));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            return tmp.ToString();
        }

        /// <summary>
        /// 编码：Base64编码
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string ToBase64String(this string m)
        {
            if (m.isNull())
            {
                return "";
            }
            byte[] bytes = Encoding.Default.GetBytes(m);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 解码：Base64编码
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string Base64GetString(this string m)
        {
            if (m.isNull())
            {
                return "";
            }
            byte[] outputb = Convert.FromBase64String(m);
            return Encoding.Default.GetString(outputb);
        }
        public static string Replace(this string m, List<string> toReplace, string foValue = "")
        {
            if (m.isNull())
            {
                return "";
            }
            foreach (var item in toReplace)
            {
                m.Replace(item, foValue);
            }
            return m;
        }
        public static bool isNull(this string m)
        {
            if (string.IsNullOrEmpty(m) || string.IsNullOrWhiteSpace(m))
            {
                return true;
            }
            return false;
        } 
        /// <summary>
        /// 重复生成记录空白
        /// </summary>
        /// <param name="m"></param>
        /// <param name="genNum"></param>
        /// <param name="formatStr"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string StrFormatNum(this string m, int genNum = 3, string formatStr = "   {0}   {1}", char splitChar = '、')
        {
            if (m == null)
            {
                m = m.NullToStr();
            }
            var splitResult = m.SplitTrim(splitChar);
            string result = "";
            for (int i = 0; i < genNum; i++)
            {
                string nameValue = "      ";
                if (i < splitResult.Count)
                {
                    nameValue = splitResult[i];
                }
                result += string.Format(formatStr, nameValue, splitChar);
            }
            return result.Trim(splitChar);
        }
        /// <summary>
        /// 重复生成字符
        /// </summary>
        /// <param name="m"></param>
        /// <param name="genNum"></param>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        public static string StrFormatNum(this string m, double genNum = 3, string formatStr = " ")
        {
            if (m == null)
            {
                m = m.NullToStr();
            }
            string result = "";
            for (int i = 0; i < genNum; i++)
            {
                result += formatStr;
            }
            return result;
        }
        /// <summary>
        /// 去空，去空格
        /// </summary>
        /// <param name="m"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static List<string> SplitTrim(this string m, params char[] separator)
        {
            if (m == null)
            {
                return null;
            }
            var result = new List<string>();
            var resultSplit = m.Split(separator);
            foreach (var item in resultSplit)
            {
                if (!item.isNull())
                {
                    result.Add(item.Trim());
                }
            }
            return result;
        }

        #endregion
        /// <summary>
        /// 获取1M大小字符
        /// 1M = 1024k = 1048576字节 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetOneM(this string text, string defaultStr = "0")
        {
            if (text.IsNullOrWhiteSpace())
            {
                text = defaultStr;
            }
            StringBuilder sb = new StringBuilder();
            int getByte = GetBytesOfString(text);
            int getnCount = 1048576 / getByte;
            for (int i = 0; i < getnCount; i++)
            {
                sb.Append(text);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取输入数据字节大小 返回大小
        /// 1M = 1024k = 1048576字节 
        /// 1:m,2:k,3:byte
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static int GetBytesOfString(this string Text, int type = 3)
        {
            int nByte = 0;
            byte[] bytes = Encoding.Unicode.GetBytes(Text);
            for (int i = 0; i < bytes.GetLength(0); i++)
            {
                //  偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节
                if (i % 2 == 0)
                {
                    nByte++;      //  在UCS2第一个字节时n加1
                }
                else
                {
                    //  当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节
                    if (bytes[i] > 0)
                    {
                        nByte++;
                    }
                }
            }
            int result = nByte;
            switch (type)
            {
                case 1:
                    result = nByte / 1048576;
                    break;
                case 2:
                    result = nByte / 1024;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
