using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public class ConstantValues
    {
        
        public const string VaiTroQuanTriHeThong = "5872e91b7600651905cc82b5";
        public const string SessionKeyCurrentUser = "CurrentUser";
        public const string SessionKeyVaiTro = "VaiTroHeThong";
        public const string SessionKeyUserId = "userID";
        public const string SessionKeyMenu = "Menu";
        public const string SessionKeyUrl = "Url";

        public const string MaPhanCachGiaTri = "|pmks|";
        public const string MaPhanCachTuNguoiDung = "|||";

    }
    public class DinhDanhNoiDungMail
    {
        public const string HoTenKhachHang = "[HoTen]";
        public const string DienThoaiKhachHang = "[DienThoai]";
        public const string EmailKhachHang = "[Email]";
        public const string TenDonVi = "[TenĐonVi]";
        public const string SoDienThoaiDonVi = "[SDTDonVi]";
        public const string EmailDonVi = "[EmailDonVi]";
       
    }
    public class MaLoaiCauHoi
    {
        public const string DungSai = "truefalse";
        public const string ChamDiem = "singlechoice";
        public const string NhieuLuaChon = "multichoice";
        public const string TuLuan = "essay";
    }
    public class TenLoaiCauHoi
    {
        public const string DungSai = "Đúng/Sai";
        public const string ChamDiem = "Chấm điểm";
        public const string NhieuLuaChon = "Nhiều lựa chọn";
        public const string TuLuan = "Tự luận";
    }
    public class GiaTri
    {
        public const string Dung = "Đúng";
        public const string Sai = "Sai";
        public const string Co = "Có";
        public const string Khong = "Không";
    }
    public class GiaTriBHYT
    {
        public const string DungChung = "Dùng chung";
        public const string KhongBHYT = "Không BHYT";
        public const string CoBHYT = "Có BHYT";

    }
    public class LoaiKhaoSat
    {
        public const string PhieuKhaoSatNhanh = "PhieuKhaoSatNhanh";
        public const string PhieuKhaoSat = "PhieuKhaoSat";
        public const string DanhGiaTraiNghiem = "DanhGiaTraiNghiem";
    }
    public class MaThongTinNguoiDung
    {
        public const string Ten = "Ten";
        public const string Tuoi = "Tuoi";
        public const string SoDienThoai = "SoDienThoai";
        public const string GioiTinh = "GioiTinh";
        public static Dictionary<string, string> DanhSachThongTinNguoiDung() {
            return new Dictionary<string, string>() {
                    { Ten , "Tên" },
                    { Tuoi , "Tuổi" },
                    { SoDienThoai , "Số điện thoại" },
                    { GioiTinh , "Giới tính" } };
        }
    }
    public class APIUrl
    {
        public static string ResouceUrl = ConfigurationSettings.AppSettings["PMKS_SystemWebUI"]; //"http://172.29.14.66:8899/";
        public static string BaseUrl = ConfigurationSettings.AppSettings["PMKS_WebAPI"]; //"http://221.132.18.21:9999/";
        public const string DocDanhSachDotKhaoSat = "api/DotKhaoSat/DocDanhSachDotKhaoSat";
        public const string DocDonViTheoTenKiosk = "api/DonVi/DocDonViTheoTenKiosk";
        public const string DocDotKhaoSatTheoMa = "api/DotKhaoSat/DocTheoMa";
        public const string GhiBaiKhaoSat = "api/BaiKhaoSat/Ghi";
        public static string getFullUrl(string url)
        {
            return BaseUrl + url;
        }

    }


}
