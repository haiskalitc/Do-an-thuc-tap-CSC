using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model.BaseModel;

namespace Core.Model
{
    public class QuyenHan : DoiTuongMau
    {
        [BsonElement("ten_quyen")]
        public string TenQuyen { get; set; }
        [BsonElement("mo_ta")]
        public string MoTa { get; set; }
    }
}
