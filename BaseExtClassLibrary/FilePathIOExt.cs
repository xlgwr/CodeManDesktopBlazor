using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{ 
    public static class FilePathIOExt
    {
        /// <summary>
        /// temp目录下生成临时文件目录
        /// </summary>
        /// <returns></returns>
        public static string CreateTempPath(this string pathname)
        {
            string temPath = System.IO.Path.GetTempPath();
            while (!temPath.ToLower().EndsWith("temp"))
            {
                temPath = System.IO.Directory.GetParent(temPath).FullName;
            }
            string path = Path.Combine(temPath, pathname);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }
        public static bool DirectoryExists(this string path)
        {
            return System.IO.Directory.Exists(path);
        }
        public static bool DirectoryCreate(this string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            };
            return true;
        }
        /// <summary>
        /// 文件路径获取内容，（文件）,获取前N行
        /// -1 取所有行
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static List<string> FilePathBeforeLine(this string m, int beforeLine, Encoding encoder)
        {
            var result = new List<string>();
            try
            {
                if (!File.Exists(m))
                {
                    return result;
                }

                int i = 0;
                FileStream fs = new FileStream(m, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                string currLinestr = string.Empty;
                using (StreamReader sr = new StreamReader(fs, encoder))
                {
                    while (sr.Peek() > -1)
                    {
                        currLinestr = sr.ReadLine();
                        if (i < beforeLine || beforeLine == -1)
                        {
                            result.Add(currLinestr);
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        // <summary>
        /// 倒序读取文本文件
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="position">读取流的起始位置</param>
        /// <returns></returns>
        public static List<string> FilePathLastLine(this string file, int endNline, Encoding encoder, string searchStr = "")
        {

            var result = new List<string>();

            if (!File.Exists(file))
            {
                return result;
            }
            long ps = 0;
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (ps <= 0)
                    ps = fs.Length;
                //读取n行
                for (int i = 0; i <= endNline; i++)
                {
                    var getresult = InverseReadRow2(fs, ps, ref result, searchStr);

                    if (!result.HasItem())
                    {
                        endNline++;
                    }
                    ps = getresult.Item1;

                    if (ps <= 0)
                        break;

                    if (result.Count == endNline)
                    {
                        break;
                    }

                    if (getresult.Item2.IsNotNullOrWhiteSpace())
                    {
                        break;
                    }
                    else
                    {
                        if (searchStr.IsNotNullOrWhiteSpace())
                        {
                            endNline++;
                        }
                    }


                }
            }
            return result;
        }
        /// <summary>
        /// 从后向前按行读取文本文件，最多读取 100kb
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="position"></param>
        /// <param name="s"></param>
        /// <param name="maxRead">默认每次最多读取100kb数据</param>
        /// <returns>返回读取位置</returns>
        public static Tuple<long, string> InverseReadRow2(FileStream fs, long position
             , ref List<string> s
             , string findstr = ""
             , int maxRead = 1024000)
        {
            //byte n = 0xD;//回车符
            byte r = 0xA;//换行符
            if (fs.Length == 0)
                return new Tuple<long, string>(0, "");
            var newPos = position - 1;
            int curVal = 0;
            var readLength = 0;
            var str = "";
            do
            {
                readLength++;
                if (newPos <= 0)
                    newPos = 0;

                fs.Position = newPos;
                curVal = fs.ReadByte();
                if (curVal == -1)
                    break;
                if (curVal != r)
                    newPos--;

                if (newPos <= 0)
                    break;
                //
                if (readLength == maxRead)
                    break;

            } while (curVal != r);

            byte[] arr = new byte[readLength];
            fs.Position = position - readLength;
            fs.Read(arr, 0, readLength);
            str = Encoding.UTF8.GetString(arr);

            string findStr = string.Empty;
            if (!str.IsNullOrWhiteSpace() && readLength > 1)
            {
                s.Insert(0, str);
                if (!findstr.IsNullOrWhiteSpace())
                {
                    if (str.Contains(findstr))
                    {
                        findStr = str;
                    }
                }
            }
            return new Tuple<long, string>(newPos, findStr);
        }

        public static List<string> GetDirectoryOfFullName(this string path, List<string> nameFilter, string extensionFilte = ".csv")
        {

            var result = new List<string>();
            if (path.IsNullOrWhiteSpace() || !path.DirectoryExists())
            {
                return result;
            }
            try
            {
                DirectoryInfo fdir = new DirectoryInfo(path);//文件列表   
                var file = fdir.GetFiles().Select(a => a.FullName).ToList();
                fdir = null;
                if (file.Count == 0) //当前目录文件或文件夹不为空                   
                {
                    return result;
                }
                var currname = string.Empty;
                var currnameext = string.Empty;
                foreach (var f in file) //显示当前目录所有文件   
                {
                    currname = Path.GetFileNameWithoutExtension(f);
                    currnameext = Path.GetExtension(f).NullToStr().ToLower();
                    if (currname.IsNullOrWhiteSpace())
                    {
                        continue;
                    }
                    if (nameFilter.HasItem())
                    {
                        if (!currname.Contains(nameFilter))
                        {
                            continue;
                        };
                    }
                    if (extensionFilte.IsNullOrWhiteSpace())
                    {
                        result.Add(f);
                    }
                    else if (extensionFilte == currnameext)
                    {
                        result.Add(f);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

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
