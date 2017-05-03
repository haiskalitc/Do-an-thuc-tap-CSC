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
    public class XuLyDonVi : XuLyDuLieu<DonVi>, IXuLyDonVi
    {
        
        //public List<DonVi> DocDanhSachTuDonViCha(string donViId,  bool quanTriHeThong = false)
        //{
        //    var sort = Builders<DonVi>.Sort.Ascending(c => c.Cap).Ascending(c => c.Ten);
        //    if (quanTriHeThong)
        //    {
        //        return _collection.Find(new BsonDocument()).Sort(sort).ToList();
        //    }
        //    FilterDefinition<DonVi> dieukienloc = Builders<DonVi>.Filter.Eq(c => c.IdDonViTrucThuoc, donViId) |
        //                                            Builders<DonVi>.Filter.Eq(c => c.Id, new ObjectId(donViId));
        //    return _collection.Find(dieukienloc).Sort(sort).ToList();
        //}
        public List<DonVi> DocDanhSachTuDonViCha(string donViId, bool quanTriHeThong = false)
        {
            var sort = Builders<DonVi>.Sort.Ascending(c => c.Cap).Ascending(c => c.Ten);
            if (quanTriHeThong)
            {
                return _collection.Find(new BsonDocument()).Sort(sort).ToList();
            }
            List<DonVi> donViList = new List<DonVi>();
            donViList.Add(Doc(donViId));
            DocDanhSachDonViCon(donViId,donViList);
            return donViList;
        }
        public void DocDanhSachDonViCon(string donViId, List<DonVi> donViList)
        {
            var donViCons = DocDanhSach(dv => dv.IdDonViTrucThuoc == donViId);
            if (donViCons.Count() > 0)
            {
                foreach(var dvc in donViCons)
                {
                    donViList.Add(dvc);
                    DocDanhSachDonViCon(dvc.Id.ToString(), donViList);
                }
            }
        }
        public List<DonVi> DocDanhSachTuDonViCha(string donViId,  int soLuong, int batDau, bool quanTriHeThong = false)
        {
            var sort = Builders<DonVi>.Sort.Ascending(c => c.Cap).Ascending(c => c.Ten);
            if (quanTriHeThong)
            {
                return _collection.Find(new BsonDocument()).Sort(sort).Skip(batDau).Limit(soLuong).ToList();
            }
            FilterDefinition<DonVi> dieukienloc = Builders<DonVi>.Filter.Eq(c => c.IdDonViTrucThuoc, donViId) |
                                                    Builders<DonVi>.Filter.Eq(c => c.Id, new ObjectId(donViId)) ;
            return _collection.Find(dieukienloc).Sort(sort).Skip(batDau).Limit(soLuong).ToList();
       
        }
        

        public bool Xoa(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;
            var donVi = Doc(id);
            if (donVi == null) return false;

            //Nếu đơn vị đang được sử dụng thì ko được phép xóa
            XuLyNguoiDung xlNguoiDung = new XuLyNguoiDung();
            var nd = xlNguoiDung.DocDanhSach(n => n.IdDonVi == id);
            if (nd.Count() > 0) return false;
            XuLyNhomNguoiDung xlNhomNguoiDung = new XuLyNhomNguoiDung();
            var nnd = xlNhomNguoiDung.DocDanhSach(n => n.IdDonVi == id);
            if (nnd.Count() > 0) return false;
  
            return Xoa(donVi);
        }

        public DonVi DocDonViSo()
        {
            try
            {
                var dieukienloc = Builders<DonVi>.Filter.Eq(c => c.Cap, 1);
                return Doc(dieukienloc);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }

       
    }
}
