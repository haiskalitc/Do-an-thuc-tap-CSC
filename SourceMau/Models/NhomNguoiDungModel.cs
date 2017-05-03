using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemWebUI.Models
{
    public class NhomNguoiDungModel
    {
        //danh cho list
        public List<NhomNguoiDung> DanhSachNhomNguoiDung;
        public int TotalPage;
        public int CurrentPage;
        //danh cho edit
        public NhomNguoiDung NhomNguoiDungHienTai;
        public List<DonVi> DanhSachDonVi;
        public List<MenuQuyenHanModel> DanhSachChucNang;
    }
}