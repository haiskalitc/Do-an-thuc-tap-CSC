using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IXuLyChucNang : IXuLyDuLieu<ChucNang>
    {
        /// <summary>
        /// dành cho phân quyền với cương vị là quản trị hệ thống
        /// </summary>
        /// <returns></returns>
        List<ChucNang> DocDanhSachChucNang();
        /// <summary>
        /// dành để đọc ra danh sách chức năng cấp cha ( cấp 1)
        /// </summary>
        /// <returns></returns>
        List<ChucNang> DocDanhSachChucNangCapCha();
        /// <summary>
        /// dành cho quản trị hệ thống và developer để show o trang danh sach chức năng
        /// </summary>
        /// <returns></returns>
        List<ChucNang> DocDanhSachTatCaChucNang();
        /// <summary>
        /// ọc danh sách chức năng dựa trên danh sách chức năng của người dùng
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        List<ChucNang> DocDanhSachTatCaChucNangTuDanhSachId(List<string> idList);
        /// <summary>
        /// Khi xóa chức năng cha thì sẽ cập nhật chức năng con lên làm gốc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Xoa(string id);
    }
}
