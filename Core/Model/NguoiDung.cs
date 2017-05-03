using MongoDB.Bson.Serialization.Attributes;
using Core.Model.BaseModel;
using System.Collections.Generic;
using Common.Utilities;

namespace Core.Model
{
    /*
        Ten: (chuỗi) Họ tên người dùng 
        Ten_dang_nhap: (chuỗi) Tên đăng nhập của người dùng 
        Mat_khau: (chuỗi mã hóa) Mật khẩu đăng nhập của người dùng 
        Email: (chuỗi) địa chỉ Email của người dùng 
        Dien_thoai: (chuỗi) Điện thoại liên hệ của người dùng 
        ID_Don_vi: (số) ID của đơn vị trực thuộc 
        ID_Vai_tro: (số) ID Vai trò  
        DS_ID_Chuc_nang: (chuỗi) danh sách ID chức năng được cấp quyền 
        Kich_hoat: (chuỗi số): 1: kích hoạt, 0: không kích hoạt 
     */
    public class NguoiDung:DoiTuongMau
    {
        [BsonElement("ten")]
        public string Ten { get; set; }

        [BsonElement("ten_dang_nhap")]
        public string TenDangNhap { get; set; }

        [BsonElement("mat_khau")]
        public string MatKhau { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("dien_thoai")]
        public string DienThoai { get; set; }

        [BsonElement("id_don_vi")]
        public string IdDonVi { get; set; }

        [BsonElement("id_vai_tro")]
        public string IdVaiTro { get; set; }

        [BsonElement("danh_sach_chuc_nang")]
        public List<string> DanhSachChucNang { get; set; }

        [BsonElement("danh_sach_chuc_nang_theo_ca_nhan")]
        public List<string> DanhSachChucNangTheoCaNhan { get; set; }

        [BsonElement("danh_sach_chuc_nang_theo_nhom")]
        public List<string> DanhSachChucNangTheoNhom { get; set; }

        [BsonElement("danh_sach_nhom")]
        public List<string> DanhSachNhom { get; set; }

        [BsonElement("kich_hoat")]
        public int KichHoat { get; set; }

        [BsonIgnore]
        public DonVi DonVi { get; set; }

        [BsonIgnore]
        public VaiTro VaiTro { get; set; }
        
        //[BsonElement("hinh_dai_dien")]
        //public string HinhDaiDien { get; set; }

        public NguoiDung()
        {
            DanhSachNhom = new List<string>();
            DanhSachChucNang = new List<string>();
            DonVi = new DonVi();
            VaiTro = new VaiTro();
        }
        public void setMatKhau(string matKhau)
        {
            this.MatKhau = Utility.MD5(matKhau);
        }
    }
}
