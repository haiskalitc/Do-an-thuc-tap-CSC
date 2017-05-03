using MongoDB.Bson.Serialization.Attributes;
using Core.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    /*
    Ten: (chuỗi) Tên đơn vị (không được rỗng) 
    Dia_chi: (chuỗi) địa chỉ của đơn vị 
    Dien_thoai: (chuỗi) điện thoại liên hệ của đơn vị 
    Email: (chuỗi) email liên hệ của đơn vị 
    Fax: (chuỗi) số Fax của đơn vị 
    Logo: (chuỗi) đường dẫn hình Logo của đơn vị 
    Cau_hinh_email: (chuỗi) Thông tin cấu hình gửi email (ví dụ mẫu: {smtp_server: smtp.gmail.com, port: 25, ssl: true, username: abc@gmail.com, password: 123456, email_gui: xyz@gmail.com, ten_hien_thi: Bệnh viện XYZ} 
    */
    public class DonVi:DoiTuongMau
    {
        [BsonElement("ten")]
        public string Ten { get; set; }

        [BsonElement("dia_chi")]
        public string DiaChi { get; set; }

        [BsonElement("dien_thoai")]
        public string DienThoai { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("fax")]
        public string Fax { get; set; }

        [BsonElement("logo")]
        public string Logo { get; set; }

        //[BsonElement("ho_ten_lanh_dao")]
        //public string HoTenLanhDao { get; set; }

        //[BsonElement("dien_thoai_lanh_dao")]
        //public string DienThoaiLanhDao { get; set; }

        //[BsonElement("email_lanh_dao")]
        //public string EmailLanhDao { get; set; }
        [BsonElement("id_don_vi_truc_thuoc")]
        public string IdDonViTrucThuoc { get; set; }

        [BsonIgnore]
        public DonVi DonViTrucThuoc { get; set; }

        [BsonElement("cap")]
        public int Cap { get; set; }

        [BsonElement("cau_hinh_email")]
        public CauHinhEmail CauHinhEmail { get; set; }
        public DonVi()
        {
            CauHinhEmail = new CauHinhEmail();

        }
    }
    public class CauHinhEmail
    {
        [BsonElement("smtp_server")]
        public string SMTPServer { get; set; }

        [BsonElement("port")]
        public int Port { get; set; }

        [BsonElement("ssl")]
        public int SSL { get; set; }

        [BsonElement("tai_khoan")]
        public string TaiKhoan { get; set; }

        [BsonElement("mat_khau")]
        public string MatKhau { get; set; }

        [BsonElement("email_gui")]
        public string EmailGui { get; set; }

        [BsonElement("ten_hien_thi")]
        public string TenHienThi { get; set; }
    }
}
