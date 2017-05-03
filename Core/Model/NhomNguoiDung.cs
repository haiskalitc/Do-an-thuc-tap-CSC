using MongoDB.Bson.Serialization.Attributes;
using Core.Model.BaseModel;
using System.Collections.Generic;

namespace Core.Model
{
    /*
     * ten: "S4T Cà Mau",
     * mo_ta: "Sở Thông Tin và Truyền Thông Cà Mau"
     */
    public class NhomNguoiDung:DoiTuongMau
    {
        [BsonElement("ten")]
        public string Ten { get; set; }

        [BsonElement("mo_ta")]
        public string MoTa { get; set; }

        [BsonElement("id_don_vi")]
        public string IdDonVi { get; set; }

        [BsonElement("danh_sach_nguoi_dung")]
        public List<string> DanhSachNguoiDung { get; set; }

        [BsonElement("danh_sach_chuc_nang")]
        public List<string> DanhSachChucNang { get; set; }
        [BsonIgnore]
        public string TenDonVi { get; set; }
        public NhomNguoiDung()
        {
            DanhSachChucNang = new List<string>();
            DanhSachNguoiDung = new List<string>();
        }
    }
}
