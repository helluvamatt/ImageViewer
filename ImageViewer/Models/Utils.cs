using System.Collections.Generic;
using System.Linq;

namespace ImageViewer.Models
{
    internal static class LinqUtils
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            T[] bucket = null;
            var count = 0;
            foreach (var item in source)
            {
                if (bucket == null) bucket = new T[size];
                bucket[count++] = item;
                if (count < size) continue;
                yield return bucket;
                bucket = null;
                count = 0;
            }
            if (bucket != null && count > 0) yield return bucket.Take(count);
        }
    }

    internal static class FileUtils
    {
        public static string GetFileSizeString(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }
    }
}
