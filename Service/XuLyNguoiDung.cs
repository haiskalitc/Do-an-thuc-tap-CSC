using MongoDB.Bson;
using MongoDB.Driver;
using Common.Enum;
using Core.Interface.Service;
using Core.Model;
using Service.BaseService;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class XuLyNguoiDung : XuLyDuLieu<NguoiDung>, IXuLyNguoiDung
    {
        public NguoiDung DangNhap(string tenDangNhap, string matKhau)
        {
            var dieuKienLoc = Builders<NguoiDung>.Filter.Eq(p => p.TenDangNhap, tenDangNhap) 
                & Builders<NguoiDung>.Filter.Eq(p => p.MatKhau, matKhau) 
                & Builders<NguoiDung>.Filter.Eq(p => p.KichHoat,1);
            var nguoiDung = Doc(dieuKienLoc);
            if(nguoiDung != null)
            {
                XuLyDonVi xlDonVi = new XuLyDonVi();
                XuLyVaiTro xlVaiTro = new XuLyVaiTro();
                nguoiDung.DonVi = xlDonVi.Doc(nguoiDung.IdDonVi);
                nguoiDung.VaiTro = xlVaiTro.Doc(nguoiDung.IdVaiTro);
            }
            
            return nguoiDung;
        }
        public List<NguoiDung> DocDanhSachCungTenDonVi(int soLuong, int batDau, string tuKhoa = "", string donViId = "0")
        {
            List<NguoiDung> nguoiDungs;
            XuLyDonVi xlDonVi = new XuLyDonVi();
            XuLyVaiTro xlVaiTro = new XuLyVaiTro();
            if (donViId != "0")//quản trị sẽ chỉ thấy user của đơn vị mình
            {
                var donViList = xlDonVi.DocDanhSachTuDonViCha(donViId);
                FilterDefinition<NguoiDung> dieukienloc;
                if (donViList.Count > 0)
                {
                    dieukienloc = Builders<NguoiDung>.Filter.Eq(n => n.IdDonVi, donViList.ElementAt(0).Id.ToString());
                    for (int i = 1; i < donViList.Count; i++)
                    {
                        dieukienloc |= Builders<NguoiDung>.Filter.Eq(n => n.IdDonVi, donViList.ElementAt(i).Id.ToString());
                    }
                }
                else
                {
                    dieukienloc = Builders<NguoiDung>.Filter.Eq(n => n.IdDonVi, donViId);
                }
                if(tuKhoa != "")
                {
                    dieukienloc = (dieukienloc) & (Builders<NguoiDung>.Filter.Regex(n => n.Email, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.DienThoai, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.TenDangNhap, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.Ten, new BsonRegularExpression(tuKhoa)));
                }
                dieukienloc = (dieukienloc) & Builders<NguoiDung>.Filter.Ne(n => n.IdVaiTro, ConstantValues.VaiTroQuanTriHeThong);
                nguoiDungs = _collection.Find(dieukienloc).Skip(batDau).Limit(soLuong).ToList();
                foreach (var n in nguoiDungs)
                {
                    var vt = xlVaiTro.Doc(n.IdVaiTro);
                    if (vt != null)
                    {
                        n.VaiTro = vt;
                    }
                    var dv = donViList.Find(d => d.Id.ToString() == n.IdDonVi);
                    if (dv != null)
                    {
                        n.DonVi = dv;
                    }
                    
                }

            }
            else//nếu là quản trị hệ thống sẽ thấy hết tất cả user
            {
                if (tuKhoa != "")
                {
                    FilterDefinition<NguoiDung> dieukienloc = (Builders<NguoiDung>.Filter.Regex(n => n.Email, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.DienThoai, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.TenDangNhap, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.Ten, new BsonRegularExpression(tuKhoa)));
                    nguoiDungs = _collection.Find(dieukienloc).Skip(batDau).Limit(soLuong).ToList();
                }
                else
                {
                    nguoiDungs = _collection.Find(new BsonDocument()).Skip(batDau).Limit(soLuong).ToList();
                }
                foreach (var n in nguoiDungs)
                {
                    var dv = xlDonVi.Doc(n.IdDonVi);
                    if (dv != null)
                    {
                        n.DonVi = dv;
                    }
                    var vt = xlVaiTro.Doc(n.IdVaiTro);
                    if (vt != null)
                    {
                        n.VaiTro = vt;
                    }
                }
            }
            
            return nguoiDungs;
        }
        public long TongSoLuong(string tuKhoa = "", string donViId = "0")
        {
            XuLyDonVi xlDonVi = new XuLyDonVi();
            if (donViId != "0")//quản trị sẽ chỉ thấy user của đơn vị mình
            {
                var donViList = xlDonVi.DocDanhSachTuDonViCha(donViId);
                FilterDefinition<NguoiDung> dieukienloc;
                if (donViList.Count > 0)
                {
                    dieukienloc = Builders<NguoiDung>.Filter.Eq(n => n.IdDonVi, donViList.ElementAt(0).Id.ToString());
                    for (int i = 1; i < donViList.Count; i++)
                    {
                        dieukienloc |= Builders<NguoiDung>.Filter.Eq(n => n.IdDonVi, donViList.ElementAt(i).Id.ToString());
                    }
                }
                else
                {
                    dieukienloc = Builders<NguoiDung>.Filter.Eq(n => n.IdDonVi, donViId);
                }
                if (tuKhoa != "")
                {
                    dieukienloc = (dieukienloc) & (Builders<NguoiDung>.Filter.Regex(n => n.Email, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.DienThoai, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.TenDangNhap, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.Ten, new BsonRegularExpression(tuKhoa)));
                }
                dieukienloc = (dieukienloc) & Builders<NguoiDung>.Filter.Ne(n => n.IdVaiTro, ConstantValues.VaiTroQuanTriHeThong);
                return _collection.Find(dieukienloc).Count();
            }
            else//nếu là quản trị hệ thống sẽ thấy hết tất cả user
            {
                if (tuKhoa != "")
                {
                    FilterDefinition<NguoiDung> dieukienloc = (Builders<NguoiDung>.Filter.Regex(n => n.Email, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.DienThoai, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.TenDangNhap, new BsonRegularExpression(tuKhoa))
                        | Builders<NguoiDung>.Filter.Regex(n => n.Ten, new BsonRegularExpression(tuKhoa)));
                    return _collection.Find(dieukienloc).Count();
                }
                else
                {
                    return base.TongSoLuong();
                }
                
            }
        }
        //public List<NguoiDung> LayDanhSachNguoiDungTimKiem(string tuKhoa)
        //{
        //    var dieukienloc = Builders<NguoiDung>.Filter.Regex("email", new BsonRegularExpression(tuKhoa))
        //        | Builders<NguoiDung>.Filter.Regex("so_dien_thoai", new BsonRegularExpression(tuKhoa))
        //        | Builders<NguoiDung>.Filter.Regex("ten_dang_nhap", new BsonRegularExpression(tuKhoa));
        //    var results = DocDanhSach(dieukienloc);
        //    return results.ToList();
        //}
    }
}