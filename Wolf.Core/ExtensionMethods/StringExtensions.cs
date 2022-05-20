using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Wolf.Core.ExtensionMethods
{
    public static class StringExtensions
    {
        public static T AsObject<T>(this string str)
        {            
            var obj = JsonConvert.DeserializeObject<T>(str);
            return obj;
        }
        public static string MultiInsert(this string str, string insertChar, int numChar, params int[] positions)
        {
            if (numChar == 0 || string.IsNullOrEmpty(insertChar) || positions.Count() == 0) return str;            
            StringBuilder sb = new StringBuilder(str.Length + (positions.Count() * insertChar.Length * numChar));
            var posLookup = new HashSet<int>(positions);
            for (int i = 0; i < str.Length; i++)
            {                
                if (posLookup.Contains(i))
                {
                    for (int j = 0; j < numChar; j++)
                    {
                        sb.Append(insertChar);
                    }
                }
                sb.Append(str[i]);
            }
            return sb.ToString();
        }
        public static string Left(this string value, int length)
        {
            length = Math.Abs(length);
            return string.IsNullOrEmpty(value) ? value : value.Substring(0, Math.Min(value.Length, length));
        }

        public static string Right(this string value, int length)
        {
            length = Math.Abs(length);
            return string.IsNullOrEmpty(value) ? value : value.Substring(value.Length - Math.Min(value.Length, length));
        }

        public static bool In(this string value, List<string> list)
        {
            return list.Contains(value, StringComparer.OrdinalIgnoreCase);
        }

        public static bool NotIn(this string value, List<string> list)
        {
            return !In(value, list);
        }

        public static bool EqualsIgnoreCase(this string source, string toCheck)
        {
            return string.Equals(source, toCheck, StringComparison.OrdinalIgnoreCase);
        }

        public static string ToBase64(this string src)
        {
            byte[] b = Encoding.UTF8.GetBytes(src);
            return Convert.ToBase64String(b);
        }

        public static string ToBase64(this string src, Encoding encoding)
        {
            byte[] b = encoding.GetBytes(src);
            return Convert.ToBase64String(b);
        }

        public static string FromBase64String(this string src)
        {
            byte[] b = Convert.FromBase64String(src);
            return Encoding.UTF8.GetString(b);
        }

        public static string FromBase64String(this string src, Encoding encoding)
        {
            byte[] b = Convert.FromBase64String(src);
            return encoding.GetString(b);
        }

        public static string Remove(this string source, params string[] removedValues)
        {
            removedValues.ToList().ForEach(x => source = source.Replace(x, ""));
            return source;
        }
        public static bool ContainsAny(this string input, IEnumerable<string> containsKeywords, StringComparison comparisonType)
        {
            return containsKeywords.Any(keyword => input.IndexOf(keyword, comparisonType) >= 0);
        }
    }
}
