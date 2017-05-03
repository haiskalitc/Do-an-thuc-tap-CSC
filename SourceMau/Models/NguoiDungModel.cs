using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemWebUI.Models
{
    public class NguoiDungModel
    {
        //dành cho list
        public List<NguoiDung> DanhSachNguoiDung;
        public string DieuKienLoc;
        public int TotalPage;
        public int CurrentPage;
        public string TuKhoa;
        //Dành cho thêm và chỉnh sửa
        public NguoiDung NguoiDungHienTai;
        public List<VaiTro> DanhSachVaiTro;
        public List<DonVi> DanhSachDonVi;
        public List<NhomNguoiDung> DanhSachNhomNguoiDung;
        public List<MenuQuyenHanModel> DanhSachChucNang;
        

    }
}