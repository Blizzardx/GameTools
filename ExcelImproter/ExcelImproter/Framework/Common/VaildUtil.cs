using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameConfigTools.Util
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
        public static bool IsFormateCorrect_Int(string s)
        {
            int value = 0;
            return TryConvert(s, out value);
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
        public static bool IsFormateCorrect_Float(string s)
        {
            float value = 0;
            return TryConvert(s, out value);
        }
        #endregion

        #region short
        public static bool TryConvertShort(string s, out short n, short min = short.MinValue, short max=short.MaxValue)
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
        public static bool IsFormateCorrect_Short(string s)
        {
            short value = 0;
            return TryConvert(s, out value);
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
        public static bool IsFormateCorrect_Long(string s)
        {
            long value = 0;
            return TryConvert(s, out value);
        }
        #endregion

        #region double

        public static bool TryConvertDouble(string s, out double n, double min = double.MinValue, double max = double.MaxValue)
        {
            double a;
            n = 0;
            if (!double.TryParse(s, out a))
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
        public static bool IsFormateCorrect_Double(string s)
        {
            double value = 0;
            return TryConvert(s, out value);
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
        public static bool IsFormateCorrect_Byte(string s)
        {
            byte value = 0;
            return TryConvert(s, out value);
        }
        public static bool TryConvertsByte(string s, out sbyte n, sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
        {
            sbyte a;
            n = 0;
            if (!sbyte.TryParse(s, out a))
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
        public static bool TryConvert(string s, out sbyte n, sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
        {
            return TryConvertsByte(s, out n, min, max);
        }
        public static bool IsFormateCorrect_sByte(string s)
        {
            sbyte value = 0;
            return TryConvert(s, out value);
        }
        #endregion

        #region bool
        public static bool TryConvert(string s, out bool n)
        {
            n = false;
            if (s != "0" && s != "1")
            {
                return false;
            }
            if (s == "1")
            {
                n = true;
            }
            return true;
        }
        #endregion

        #region list
        public static List<int> SplitToList(string str)
        {
            string[] ss = str.Split('|');
            List<int> list = new List<int>();
            foreach (string s in ss)
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
            string[] ss = str.Split('|');
            List<int> list = new List<int>();
            foreach (string s in ss)
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
            string[] ss = str.Split('|');
            List<float> list = new List<float>();
            foreach (string s in ss)
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
            string[] ss = str.Split('|');
            List<string> list = new List<string>();
            foreach (string s in ss)
            {
                list.Add(s);
            }
            return list;
        }
        public static List<short> SplitToList_short(string str)
        {
            string[] ss = str.Split('|');
            List<short> list = new List<short>();
            foreach (string s in ss)
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
            string[] ss = str.Split('|');
            List<long> list = new List<long>();
            foreach (string s in ss)
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
        public static List<double> SplitToList_double(string str)
        {
            string[] ss = str.Split('|');
            List<double> list = new List<double>();
            foreach (string s in ss)
            {
                double n;
                if (!double.TryParse(s, out n))
                {
                    return null;
                }
                list.Add(n);
            }
            return list;
        }
        public static List<sbyte> SplitToList_sbyte(string str)
        {
            string[] ss = str.Split('|');
            List<sbyte> list = new List<sbyte>();
            foreach (string s in ss)
            {
                sbyte n;
                if (!sbyte.TryParse(s, out n))
                {
                    return null;
                }
                list.Add(n);
            }
            return list;
        }
        public static List<bool> SplitToList_bool(string str)
        {
            string[] ss = str.Split('|');
            List<bool> list = new List<bool>();
            foreach (string s in ss)
            {
                int n;
                if (!int.TryParse(s, out n))
                {
                    return null;
                }
                if (n != 0 || n != 1)
                {
                    return null;
                }
                list.Add(n == 1);
            }
            return list;
        }
        #endregion

        #region other
        public static bool VaildColor(string htmlStr)
        {
            if(htmlStr == null || htmlStr.Trim().Length == 0)
            {
                return true;
            }
            try
            {
                int i = Convert.ToInt32(htmlStr, 16);
                if(i > 0 && i < 0xFFFFFF)
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
                if(str == null || str.Trim() == "")
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
            if(time1 != null)
            {
                return time2 != null;
            }
            if(time1 == null)
            {
                return time2 == null;
            }
            return false;
        }
        public static string ToFormatString(this DateTime? time)
        {
            if(time == null)
            {
                return "";
            }
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", time);
        }
        public static bool IsLineNotNull(List<string> line)
        {
            if (line == null)
            {
                return false;
            }
            foreach (string obj in line)
            {
                if (obj != null && obj.ToString().Trim() != "")
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsChar(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"[a-zA-Z]");

            if (rex.IsMatch(s))
            {
                return true;
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
