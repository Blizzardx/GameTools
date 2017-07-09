using System;
using System.Collections.Generic;
using System.Linq;

//using System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Util
{
    public static class VaildUtil
    {

        #region string

        public static bool TryConvert(string s, out string n, string min, string max)
        {
            n = s;
            return true;
        }

        #endregion

        #region int

        public static bool TryConvertInt(string s, out int n)
        {
            return TryConvertInt(s, out n, 0, int.MaxValue);
        }

        public static bool TryConvertInt(string s, out int n, int min, int max)
        {
            int a;
            n = 0;
            if (!int.TryParse(s, out a))
            {
                return false;
            }
            if (a < min)
            {
                return false;
            }
            if (a > max)
            {
                return false;
            }
            n = a;
            return true;
        }

        public static bool TryConvert(string s, out int n, int min = int.MinValue, int max = int.MaxValue)
        {
            return TryConvertInt(s, out n, min, max);
        }

        #endregion

        #region float

        public static bool TryConvertFloat(string s, out float f)
        {
            return TryConvertFloat(s, out f, 0, float.MaxValue);
        }

        public static bool TryConvertFloat(string s, out float n, float min, float max)
        {
            float a;
            n = 0;
            if (!float.TryParse(s, out a))
            {
                return false;
            }
            if (a < min)
            {
                return false;
            }
            if (a > max)
            {
                return false;
            }
            n = a;
            return true;
        }

        public static bool TryConvert(string s, out float n, float min = float.MinValue, float max = float.MaxValue)
        {
            return TryConvertFloat(s, out n, min, max);
        }

        #endregion
        #region float

        public static bool TryConvertDouble(string s, out double n, double min, double max)
        {
            float a;
            n = 0;
            if (!float.TryParse(s, out a))
            {
                return false;
            }
            if (a < min)
            {
                return false;
            }
            if (a > max)
            {
                return false;
            }
            n = a;
            return true;
        }

        public static bool TryConvert(string s, out double n, double min = double.MinValue, double max = double.MaxValue)
        {
            return TryConvertDouble(s, out n, min, max);
        }

        #endregion
        #region short

        public static bool TryConvertShort(string s, out short n, short min = short.MinValue, short max = short.MaxValue)
        {
            short a;
            n = 0;
            if (!short.TryParse(s, out a))
            {
                return false;
            }
            if (a < min)
            {
                return false;
            }
            if (a > max)
            {
                return false;
            }
            n = a;
            return true;
        }

        public static bool TryConvert(string s, out short n, short min = short.MinValue, short max = short.MaxValue)
        {
            return TryConvertShort(s, out n, min, max);
        }

        #endregion

        #region long

        public static bool TryConvertLong(string s, out long n, long min = long.MinValue, long max = long.MaxValue)
        {
            long a;
            n = 0;
            if (!long.TryParse(s, out a))
            {
                return false;
            }
            if (a < min)
            {
                return false;
            }
            if (a > max)
            {
                return false;
            }
            n = a;
            return true;
        }

        public static bool TryConvert(string s, out long n, long min = long.MinValue, long max = long.MaxValue)
        {
            return TryConvertLong(s, out n, min, max);
        }

        #endregion

        #region byte

        public static bool TryConvertByte(string s, out byte n, byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            byte a;
            n = 0;
            if (!byte.TryParse(s, out a))
            {
                return false;
            }
            if (a < min)
            {
                return false;
            }
            if (a > max)
            {
                return false;
            }
            n = a;
            return true;
        }

        public static bool TryConvert(string s, out byte n, byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            return TryConvertByte(s, out n, min, max);
        }

        #endregion

        #region list

        public static List<int> SplitToList(string str)
        {
            var ss = str.Split('|');
            var list = new List<int>();
            foreach (var s in ss)
            {
                int n;
                if (!int.TryParse(s, out n))
                {
                    return null;
                }
                list.Add(n);
            }
            return list;
        }

        public static List<int> SplitToList_int(string str)
        {
            var ss = str.Split('|');
            var list = new List<int>();
            foreach (var s in ss)
            {
                int n;
                if (!int.TryParse(s, out n))
                {
                    return null;
                }
                list.Add(n);
            }
            return list;
        }

        public static List<float> SplitToList_float(string str)
        {
            var ss = str.Split('|');
            var list = new List<float>();
            foreach (var s in ss)
            {
                float n;
                if (!float.TryParse(s, out n))
                {
                    return null;
                }
                list.Add(n);
            }
            return list;
        }

        public static List<string> SplitToList_string(string str)
        {
            var ss = str.Split('|');
            var list = new List<string>();
            foreach (var s in ss)
            {
                list.Add(s);
            }
            return list;
        }

        public static List<short> SplitToList_short(string str)
        {
            var ss = str.Split('|');
            var list = new List<short>();
            foreach (var s in ss)
            {
                short n;
                if (!short.TryParse(s, out n))
                {
                    return null;
                }
                list.Add(n);
            }
            return list;
        }

        public static List<long> SplitToList_long(string str)
        {
            var ss = str.Split('|');
            var list = new List<long>();
            foreach (var s in ss)
            {
                long n;
                if (!long.TryParse(s, out n))
                {
                    return null;
                }
                list.Add(n);
            }
            return list;
        }

        public static bool TryConvert(string s, int splitType, out List<string> result, out int skipCount)
        {
            skipCount = 0;
            result = null;
            try
            {
                result = SplitToList_string(s);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool TryConvert(string[] s, int startIndex,int splitType, out List<string> result, out int skipCount)
        {
            skipCount = 0;
            result = null;

            try
            {
                if(splitType == 0)
                {
                    result = SplitToList_string(s[startIndex]);
                }
                else
                {
                    result = SpliteToList_stringByBrackets(s, startIndex, out skipCount);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static List<string> SpliteToList_stringByBrackets(string[] s, int startIndex, out int skipCount)
        {
            int markStartIndex = startIndex+1;
            skipCount = 0;
            List<string> res = new List<string>();
            if(s[startIndex] != "(")
            {
                throw new Exception("split bracket  error");
            }

            ++startIndex;
            while (true)
            {
                var elem = s[startIndex];
                ++startIndex;

                if(elem == ")")
                {
                    break;
                }
                res.Add(elem);

            }
            skipCount = startIndex - markStartIndex;
            return res;
        }
        #endregion

        #region other

        public static bool VaildColor(string htmlStr)
        {
            // FORMATE RGBA oxffffffff
            if (htmlStr == null || htmlStr.Trim().Length == 0)
            {
                return true;
            }
            try
            {
                var i = Convert.ToInt64(htmlStr, 16);
                if (i >= 0 && i <= 0xFFFFFFFF)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool VaildDateTime(string str, out DateTime? time)
        {
            return VaildDateTime(str, false, out time);
        }

        public static bool VaildDateTime(string str, bool nullable, out DateTime? time)
        {
            time = null;
            if (nullable)
            {
                if (str == null || str.Trim() == "")
                {
                    return true;
                }
            }
            try
            {
                time = DateTime.Parse(str);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool VaildDateTimePair(DateTime? time1, DateTime? time2)
        {
            if (time1 != null)
            {
                return time2 != null;
            }
            if (time1 == null)
            {
                return time2 == null;
            }
            return false;
        }

        public static string ToFormatString(this DateTime? time)
        {
            if (time == null)
            {
                return "";
            }
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", time);
        }

        public static List<List<string>> ParseBracket(int startIndex, string[] line)
        {
            var strList = new List<string>();
            for (var i = startIndex; i < line.Length; ++i)
            {
                strList.Add(line[i]);
            }

            var stack = new Stack<string>();
            var index = 0;
            var beginIndex = 0;
            var endIndex = 0;
            List<List<string>> list = null;
            foreach (var str in strList)
            {
                var s = str;
                index++;
                if (s == null)
                {
                    s = "";
                }
                s = s.Trim();
                if (s == "(")
                {
                    beginIndex = index;
                    stack.Push(str);
                    continue;
                }
                if (s == ")")
                {
                    if (stack.Count == 0)
                    {
                        return null;
                    }
                    //括号没有匹配上
                    if (stack.Pop() != "(")
                    {
                        return null;
                    }
                    endIndex = index - 1;
                    if (list == null)
                    {
                        list = new List<List<string>>();
                    }
                    var result = new List<string>();
                    list.Add(result);
                    for (var i = beginIndex; i < endIndex; i++)
                    {
                        if (strList[i] == null)
                        {
                            continue;
                        }
                        result.Add(strList[i]);
                    }
                }
            }
            //括号不匹配
            if (stack.Count > 0)
            {
                return null;
            }
            return list;
        }

        public static bool IsColor(string color)
        {
            if (color.Length != 6)
            {
                return false;
            }
            var chs = color.ToArray();
            var succCount = 0;
            foreach (var c in chs)
            {
                if ((c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F') || (c >= '0' && c <= '9'))
                {
                    succCount++;
                }
            }
            return succCount == color.Length;
        }

        #endregion

    }
}