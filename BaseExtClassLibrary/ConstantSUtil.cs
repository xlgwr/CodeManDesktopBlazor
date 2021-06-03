using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class ConstantSUtil
    {
        /// <summary>
        /// 时间格式
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public const string DateTimeFileFormat = "yyyy-MM-dd HHmmss";
        public const string DateTimeFormatShot = "yyyy-MM-dd";
        public const string DateTimeFormatSettle = "yyyyMMdd";
        public const string UserID = "UserID";
        public const string RecoverData = "RecoverData";
        public const string RecoverDataPersistent = "RecoverDataPersistent";
        public const string DBlogSuccessFlagLocal = "DBlogSuccessFlagLocal";
        public const string DBlogSuccessFlagPersistent = "DBlogSuccessFlagPersistent";
        public const decimal DEFAULT_PRICE = -999999;
        public const string NegativePrice = "NegativePrice";
        public static string NYMEX_CL { get; set; }


    }
}
