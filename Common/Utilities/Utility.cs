using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Utilities
{
    public class Utility
    {
       
        public static string EncryptDecryptKey = "Y3Njb20=";
        private static string path = @"c:\tam\LogServer.txt";
        private static object khoa = new object();
        public static void WriteLog(string s)
        {
            try
            {
                //lock (khoa)
                //{
                //    string folderPath = @"c:\tam";
                //    //check and create folder website log
                //    if (!Directory.Exists(folderPath))
                //    {
                //        // Try to create the directory.
                //        Directory.CreateDirectory(folderPath);
                //    }
                //    using (var r = File.Open(path, FileMode.Open, FileAccess.Write, FileShare.Write))
                //    {
                //        StreamWriter str = new StreamWriter(r);
                //        str.BaseStream.Seek(0, SeekOrigin.End);
                //        str.Flush();
                //        str.Write(DateTime.Now.ToString("[dd/MM/yyy hh-mm-ss] ") + s + Environment.NewLine);
                //        str.Close();
                //        r.Close();
                //    }
                //    //File.AppendAllText(path,
                //    //DateTime.Now.ToString("[dd/MM/yyy hh-mm-ss] ") + s + Environment.NewLine);
                //}
                
            }
            catch
            {
                // 
            }
        }
        /// <summary>
        ///     Chuyển các ký tự có dấu về không dấu.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToUnsign(string str)
        {
            const string pattern =
                @"([àáạảãâầấậẩẫăằắặẳẵ])|([èéẹẻẽêềếệểễ])|([ìíịỉĩ])|([òóọỏõôồốộổỗơờớợởỡ])|([ùúụủũưừứựửữ])|([ỳýỵỷỹ])|([đ])";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            string result = regex.Replace(str, delegate (Match match)
            {
                if (match.Groups[1].Value.Length > 0)
                    return "a";
                if (match.Groups[2].Value.Length > 0)
                    return "e";
                if (match.Groups[3].Value.Length > 0)
                    return "i";
                if (match.Groups[4].Value.Length > 0)
                    return "o";
                if (match.Groups[5].Value.Length > 0)
                    return "u";
                if (match.Groups[6].Value.Length > 0)
                    return "y";
                if (match.Groups[7].Value.Length > 0)
                    return "d";
                return "";
            });
            return result;
        }


        /// <summary>
        ///     Chuyển giá tiền thành chuổi ký tự.
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string PriceString(decimal? price)
        {
            string result = price == null ? "Chưa có giá" : string.Format("{0:0,0}đ", price);
            if (result.Length > 2 && result[0] == '0')
                result = result.Substring(1);
            return result;
        }

        /// <summary>
        ///     Chuyển ngày, tháng, năm, giờ, phút, giây về dạng chuổi.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="nullMessage"></param>
        /// <returns></returns>
        public static string DateTimeString(DateTime? date, string nullMessage = "")
        {
            if (date == null) return nullMessage;

            var d = (DateTime)date;
            string dayOfWeek;
            switch (d.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dayOfWeek = "Thứ 2";
                    break;
                case DayOfWeek.Tuesday:
                    dayOfWeek = "Thứ 3";
                    break;
                case DayOfWeek.Wednesday:
                    dayOfWeek = "Thứ 4";
                    break;
                case DayOfWeek.Thursday:
                    dayOfWeek = "Thứ 5";
                    break;
                case DayOfWeek.Friday:
                    dayOfWeek = "Thứ 6";
                    break;
                case DayOfWeek.Saturday:
                    dayOfWeek = "Thứ 7";
                    break;
                default:
                    dayOfWeek = "Chủ nhật";
                    break;
            }
            return string.Format(d.ToString("HH:mm:ss, {0} dd/MM/yyyy"), dayOfWeek + ", ");
        }

        /// <summary>
        ///     Chuyển thời gian về định dạng truyền vào.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="formatString"></param>
        /// <returns></returns>
        public static string DateTimeFormat(DateTime? date, string formatString = "dd/MM/yyyy")
        {
            return date == null ? "" : ((DateTime)date).ToString(formatString);
        }

        /// <summary>
        ///     Loại bỏ khoảng trắng ở 2 đầu chuổi. và thu gọn chuổi theo chiều dài định sẵn.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <param name="endString"></param>
        /// <returns></returns>
        public static string TrimString(string str, int maxLength, string endString = " ...")
        {
            if (str == null) return "";
            if (str.Length > maxLength)
                str = str.Substring(0, maxLength) + endString;
            return str;
        }

        /// <summary>
        ///     Chuyển chuổi có dấu về không dấu đồng thời các khoảng trắng được thay bằng "-", sửa dụng trong link web.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String UnsignNoSpace(String str)
        {
            string result = str.Trim().Replace(" ", "-");
            return result;
        }

        /// <summary>
        ///     Chuyển thời gian dạng timestamp về kiểu DateTime
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            //var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            // return origin.AddSeconds(timestamp);
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timestamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        ///     Chuyển kiểu DateTime về dạng timetamp
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double ConvertToUnixTimestamp(DateTime date, bool millisecond = false)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            if (!millisecond)
            {
                return Math.Floor(diff.TotalSeconds);
            }
            else
            {
                return Math.Floor(diff.TotalMilliseconds);
            }
            
        }


        public static double ConvertToUnixTimestampOriginal(DateTime date)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, date.Kind);
            long unixTimestamp = Convert.ToInt64((date - dt).TotalSeconds);
            return unixTimestamp;
        }

        /// <summary>
        ///     Mã hóa chuỗi ký tự được truyền vào kết hợp với chuổi key định sẵn để mã hóa.
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key)
        {
            try
            {
                byte[] toEncryptArray = Encoding.Unicode.GetBytes(toEncrypt);
                var hashmd5 = new MD5CryptoServiceProvider();
                byte[] keyArray = hashmd5.ComputeHash(Encoding.Unicode.GetBytes(key));
                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        ///     Giải mã một chuổi đã được mã hóa từ trước kết hợp với key đã được dùng trong lúc mã hóa.
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key)
        {
            try
            {
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
                var hashmd5 = new MD5CryptoServiceProvider();
                byte[] keyArray = hashmd5.ComputeHash(Encoding.Unicode.GetBytes(key));
                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.Unicode.GetString(resultArray);
            }
            catch
            {
                return "";
            }
        }
        
        public static string MD5(string password)
        {
            byte[] textBytes = Encoding.Default.GetBytes(password);
            try
            {
                MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                string ret = "";
                foreach (byte a in hash)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }

        static readonly string[] SizeSuffixes =
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return $"{adjustedSize:n1} {SizeSuffixes[mag]}";
        }

        public static List<DateTime> GetWeekdatesandDates(DateTime ngay_hien_tai)
        {
            List<DateTime> ds_ngay_trong_tuan = new List<DateTime>();
            DateTime mondayDate = DateTime
                           .Today
                           .AddDays(((int)(DateTime.Today.DayOfWeek) * -1) + 1);
            //add ngay trong tuan
            ds_ngay_trong_tuan.Add(mondayDate);
            for (int i = 1; i <= 6; i++)
            {
                ds_ngay_trong_tuan.Add(mondayDate.AddDays(i));
            }
            
            return ds_ngay_trong_tuan;
        }

        public static DateTime LayNgayDauTuan()
        {
            return DateTime.Today.AddDays(((int)(DateTime.Today.DayOfWeek) * -1) + 1);
        }

        public static DateTime LayNgayDauThang()
        {
            DateTime ngay_hien_tai = DateTime.Today;
            return new DateTime(ngay_hien_tai.Year, ngay_hien_tai.Month, 1);
        }

        public static DateTime LayNgayDauThangSau()
        {
            DateTime ngay_hien_tai = DateTime.Today;
            var firstDayOfMonth = new DateTime(ngay_hien_tai.Year, ngay_hien_tai.Month, 1);
            return firstDayOfMonth.AddMonths(1);
        }
        public static int GetValueCheckBox(Object obj)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()) && obj.ToString() == "on")
            {
                return 1;
            }
            return 0;
        }
        public static int ConvertIntFromString(string obj)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                return int.Parse(obj);
            }
            return 0;
        }
        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }
        public static List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }
        public static Dictionary<string, string> getValueFromJsonString(string json)
        {
            Dictionary<string, string> data =  JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            if (data == null)
                data = new Dictionary<string, string>();
            return data;
        }
    }
}