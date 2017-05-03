using MongoDB.Bson.Serialization.Attributes;
using Core.Model.BaseModel;

namespace Core.Model
{
    /*
     * ten: "Quản trị viên",
     * mo_ta: "Người có vai trò cao nhất trong hệ thống"
     */
    public class VaiTro:DoiTuongMau
    {
        [BsonElement("ten")]
        public string Ten { get; set; }

        [BsonElement("mo_ta")]
        public string MoTa { get; set; }

        [BsonElement("cap")]
        public int Cap;
        [BsonElement("icon")]
        public string Icon { get; set; }

    }
}
