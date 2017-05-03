using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Model.BaseModel
{
    public class DoiTuongMau 
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("ngay_tao")]
        public double NgayTao { get; set; }
        [BsonElement("id_nguoi_tao")]
        public string IdNguoiTao { get; set; }

        [BsonElement("ngay_cap_nhat")]
        public double NgayCapNhat { get; set; }

        [BsonElement("id_nguoi_cap_nhat")]
        public string IdNguoiCapNhat { get; set; }
    }
}