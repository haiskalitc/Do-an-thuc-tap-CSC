using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IXuLyDonVi: IXuLyDuLieu<DonVi>
    {
        /// <summary>
        /// Lấy ra danh sách đơn vị con và thông tin đơn vị của id truyền vào
        /// </summary>
        /// <param name="donViId"></param>
        /// <param name="quanTriHeThong">mặc định = false. Nếu bằng true sẽ lấy ra toàn bộ danh sách dơn vị</param>
        /// <returns></returns>
        List<DonVi> DocDanhSachTuDonViCha(string donViId, bool quanTriHeThong = false);
        List<DonVi> DocDanhSachTuDonViCha(string donViId, int soLuong, int batDau, bool quanTriHeThong = false);
        bool Xoa(string id);
        DonVi DocDonViSo();
    }
}
