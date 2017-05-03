using MongoDB.Bson.Serialization.Attributes;

using Core.Model.BaseModel;

namespace Core.Model
{
    public class PhanQuyenNguoiDung: DoiTuongMau
    {
        [BsonElement("id_quyen_han")]
        public string IdQuyen { get; set; }

        [BsonElement("id_chuc_nang")]
        public string IdChucNang { get; set; }
    }
}
