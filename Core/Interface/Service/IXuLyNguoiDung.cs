using Core.Model;
using System.Collections.Generic;

namespace Core.Interface.Service
{
    public interface IXuLyNguoiDung : IXuLyDuLieu<NguoiDung>
    {
        NguoiDung DangNhap(string tenDangNhap, string matKhau);
        List<NguoiDung> DocDanhSachCungTenDonVi(int soLuong, int batdau, string tuKhoa = "", string donViId = "0");
        long TongSoLuong(string tuKhoa = "", string donViId = "0");
        //List<NguoiDung> LayDanhSachNguoiDungTimKiem(string ten_dang_nhap, string email, string so_dien_thoai);
    }
}
