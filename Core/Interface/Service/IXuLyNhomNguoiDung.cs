using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IXuLyNhomNguoiDung:IXuLyDuLieu<NhomNguoiDung>
    {
        List<NhomNguoiDung> DocDanhSachCungTenDonVi(int soLuong, int batdau);
        bool Xoa(string id);
    }
}
