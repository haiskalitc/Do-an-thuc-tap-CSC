using MongoDB.Bson;
using MongoDB.Driver;
using Core.Interface.Service;
using Core.Model;
using Service.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class XuLyChucNang : XuLyDuLieu<ChucNang>, IXuLyChucNang
    {
        //dành cho phân quyền với cương vị là quản trị hệ thống
        public List<ChucNang> DocDanhSachChucNang()
        {
            var dieukienloc = Builders<ChucNang>.Filter.Eq(c => c.KichHoat, 1);
            var sort = Builders<ChucNang>.Sort.Ascending(c=>c.CapDo).Ascending(c => c.SapXep);
            return _collection.Find(dieukienloc).Sort(sort).ToList();
        }
        
        public List<ChucNang> DocDanhSachChucNangCapCha()
        {
            var dieukienloc = Builders<ChucNang>.Filter.Eq(c => c.CapDo, 1);
            var sort = Builders<ChucNang>.Sort.Ascending(c => c.SapXep);
            return _collection.Find(dieukienloc).Sort(sort).ToList();
        }
        //dành cho quản trị hệ thống và developer để show o trang danh sach
        public List<ChucNang> DocDanhSachTatCaChucNang()
        {
            var sort = Builders<ChucNang>.Sort.Ascending(c => c.CapDo).Ascending(c => c.SapXep);
            return _collection.Find(new BsonDocument()).Sort(sort).ToList();
        }
       //đọc danh sách chức năng dựa trên danh sách chức năng của người dùng
        public List<ChucNang> DocDanhSachTatCaChucNangTuDanhSachId(List<string> idList)
        {
            FilterDefinition<ChucNang> dieukienloc = Builders<ChucNang>.Filter.Eq(c => c.Id, new ObjectId(idList.ElementAt(0))) 
                                                    & Builders<ChucNang>.Filter.Eq(c => c.KichHoat, 1);
            for (int i = 1; i < idList.Count(); i++)
            {
                dieukienloc |= Builders<ChucNang>.Filter.Eq(c => c.Id, new ObjectId( idList.ElementAt(i)));
            }
            FilterDefinition<ChucNang> dieukien = Builders<ChucNang>.Filter.Eq(c => c.KichHoat, 1) & (dieukienloc);
            var sort = Builders<ChucNang>.Sort.Ascending(c => c.CapDo).Ascending(c => c.SapXep);
            return _collection.Find(dieukien).Sort(sort).ToList();
        }
        public bool Xoa(string id)
        {
            var chucNang = Doc(id);
            if (chucNang == null) return false;
            var chucNangCons = DocDanhSachTatCaChucNangCon(id);
            foreach(var cn in chucNangCons)
            {
                cn.IdCha = "0";
                cn.CapDo = 1;
                CapNhat(cn);
            }
            //xóa vai trò
            Xoa(chucNang);
            return true;
        }
        //đọc danh sách chức nang con để xem chức năng có con hay ko để xóa chức năng
        public List<ChucNang> DocDanhSachTatCaChucNangCon(string idCha)
        {
            var dieukienloc = Builders<ChucNang>.Filter.Eq(c => c.IdCha, idCha);
            return _collection.Find(dieukienloc).ToList();
        }
    }
}
