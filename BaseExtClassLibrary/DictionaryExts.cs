using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class DictionaryExts
    {
        #region 字典相关
        /// <summary>
        /// ConcurrentDictionary 修改
        /// </summary>
        /// <param name="keyValues"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool UpdateNewValue(this ConcurrentDictionary<string, int> keyValues, string key, int newvalue)
        {
            if (!keyValues.ContainsKey(key))
            {
                return false;
            }
            var getvalue = keyValues[key];
            return keyValues.TryUpdate(key, newvalue, getvalue);
        }
        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key) == false) dict.Add(key, value);
            return dict;
        }
        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }
        /// <summary>
        /// 获取与指定的键相关联的值，如果没有则返回输入的默认值
        /// </summary>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            return !key.NullToStr().IsNullOrWhiteSpace() && dict.ContainsKey(key) ? dict[key] : defaultValue;
        }

        public static void TryRemove<TKey, TValue>(this Dictionary<TKey, TValue> dict, List<TKey> keys)
        {
            if (dict.HasItem())
            {
                foreach (TKey item in keys)
                {
                    dict.Remove(item);
                }
            }
        }
        public static void TryRemove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, List<TKey> keys)
        {
            if (dict.HasItem())
            {
                TValue value;
                foreach (TKey item in keys)
                {
                    dict.TryRemove(item, out value);
                }
            }
        }

        #endregion
    }
}
