using System.Text.RegularExpressions;
using System.Web;
using Core.Model;
using Common.Utilities;
using Common.Enum;

namespace SystemWebUI.Migrations
{
    public static class SessionManager
    {
        //Tạo mới 1 session
        public static void RegisterSession(string key, object obj)
        {
            HttpContext.Current.Session[key] = obj;
        }

        //Hủy 1 session
        public static void FreeSession(string key)
        {
            HttpContext.Current.Session[key] = null;
        }

        //Kiểm tra session
        public static bool CheckSession(string key)
        {
            if (HttpContext.Current.Session[key] != null)
                return true;
            return false;
        }

        //Lấy giá trị của 1 session
        public static object ReturnSessionObject(string key)
        {
            if (CheckSession(key))
                return HttpContext.Current.Session[key];
            return null;
        }

        //Kiểm tra phân quyền cho user
        public static bool CheckUserIsInRole(string role, string key)
        {
            var nguoiDung = (NguoiDung)ReturnSessionObject(ConstantValues.SessionKeyCurrentUser);
            if (nguoiDung == null)
                return false;
            string[] roles = Regex.Split(role, ",");
            bool flag = false;
            foreach (string r in roles)
            {
                if (nguoiDung.IdVaiTro.ToLower().Equals(r.ToLower()))
                    flag = true;
            }
            return flag;
        }
    }
}