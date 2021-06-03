using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class ListExt
    {
        public static bool HasItem<TSource>(this IEnumerable<TSource> source)
        {

            if (source == null)
            {
                return false;
            }
            else
            {
                return source.Count() > 0;
            }
        }
        public static ObservableCollection<TSource> AddToObservableCollection<TSource>(this IEnumerable<TSource> source, ObservableCollection<TSource> target)
        {
            if (target == null || source == null)
            {
                return target;
            }
            foreach (var item in source)
            {
                target.Add(item);
            }
            return target;
        }
    }
}
