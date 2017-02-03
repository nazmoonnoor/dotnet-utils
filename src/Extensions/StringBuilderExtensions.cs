using System;
using System.Linq;
using System.Text;

namespace Web.Framework
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendNonNullOrEmpty(this StringBuilder sb, string format, object arg0, int padLeftWidth = 0)
        {
            if (arg0 == null) return sb;
            if (arg0.GetType().FullName == "System.String" && string.IsNullOrWhiteSpace(arg0.ToString()))
                return sb;
            return sb.AppendFormat(format, arg0).PadLeft(padLeftWidth);
        }

        public static StringBuilder AppendNonNullOrEmpty(this StringBuilder sb, string format, params object[] arguments)
        {
            if (arguments == null) throw new ArgumentNullException("arguments");
            bool anyNull = arguments.Any(a => a == null);
            if (anyNull) return sb;
            bool anyEmptyString = arguments.Any(a => a.GetType().FullName == "System.String" && string.IsNullOrWhiteSpace(a.ToString()));
            if (anyEmptyString) return sb;
            
            return sb.AppendFormat(format, arguments);
        }

        public static StringBuilder AppendWithLineNonNullOrEmpty(this StringBuilder sb, string format, params object[] arguments)
        {
            if (arguments == null) throw new ArgumentNullException("arguments");
            bool anyNull = arguments.Any(a => a == null);
            if (anyNull) return sb;
            bool anyEmptyString = arguments.Any(a => a.GetType().FullName == "System.String" && string.IsNullOrWhiteSpace(a.ToString()));
            if (anyEmptyString) return sb;

            return sb.AppendFormat(format, arguments).AppendLine().PadLeft(6);
        }

        public static StringBuilder PadLeft(this StringBuilder sb, int totalWidth)
        {
            string pad = string.Empty.PadLeft(totalWidth);
            return sb.Append(pad);
        }

        public static int IndexOf(this StringBuilder sb, string value, int startIndex, bool ignoreCase)
        {
            int index;
            int length = value.Length;
            int maxSearchLength = (sb.Length - length) + 1;

            if (ignoreCase)
            {
                for (int i = startIndex; i < maxSearchLength; ++i)
                {
                    if (Char.ToLower(sb[i]) == Char.ToLower(value[0]))
                    {
                        index = 1;
                        while ((index < length) && (Char.ToLower(sb[i + index]) == Char.ToLower(value[index])))
                            ++index;

                        if (index == length)
                            return i;
                    }
                }
                return -1;
            }

            for (int i = startIndex; i < maxSearchLength; ++i)
            {
                if (sb[i] == value[0])
                {
                    index = 1;
                    while ((index < length) && (sb[i + index] == value[index]))
                        ++index;

                    if (index == length)
                        return i;
                }
            }
            return -1;
        }

        public static StringBuilder RemoveLast(this StringBuilder sb, string value)
        {
            if(sb.Length < 1) return sb;
            if (!sb.ToString().Trim().EndsWith(",")) return sb;
            sb.Remove(sb.ToString().LastIndexOf(value), value.Length);
            return sb;
        }
    }
}
