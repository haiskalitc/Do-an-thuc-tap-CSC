using MongoDB.Bson.Serialization.Attributes;
using Core.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class ChucNang:DoiTuongMau
    {
        /*Id: (chuỗi) tự động phát sinh  
        Id_cha: (chuỗi) mã định dạng của chức năng cha 
        Ten: (chuỗi) Tên chức năng 
        Bieu_tuong: (chuỗi) mã số biểu tượng (class font awesome hoặc bootstrap) 
        Lien_ket: (chuỗi) đường dẫn liên kết  
        Kich_hoat: (chuỗi) kích hoạt sử dụng chức năng 
        Sap_xep: (số) mặc định là 0 
        Ngay_tao: (ngày giờ) ngày tạo  
        Nguoi_tao: (số) ID của người tạo 
        Ngay_cap_nhat: (ngày giờ) ngày cập nhật 
        Nguoi_cap_nhat: (số) ID của người cập nhật 
        */
        [BsonElement("id_cha")]
        public string IdCha { get; set; }

        [BsonElement("ten")]
        public string Ten { get; set; }

        [BsonElement("bieu_tuong")]
        public string BieuTuong { get; set; }

        [BsonElement("lien_ket")]
        public string LienKet { get; set; }

        [BsonElement("kich_hoat")]
        public int KichHoat { get; set; }

        [BsonElement("cap_do")]
        public int CapDo { get; set; }

        [BsonElement("sap_xep")]
        public int SapXep { get; set; }
        public ChucNang()
        {
            CapDo = 1;
            SapXep = 1;
        }
    }
}
