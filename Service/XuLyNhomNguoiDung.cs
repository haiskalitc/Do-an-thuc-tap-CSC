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
    public class XuLyNhomNguoiDung : XuLyDuLieu<NhomNguoiDung>, IXuLyNhomNguoiDung
    {
        public List<NhomNguoiDung> DocDanhSachCungTenDonVi(int soLuong, int batdau)
        {
            var nhomNguoiDung = DocDanhSach(soLuong,batdau).ToList();
            XuLyDonVi xlDonVi = new XuLyDonVi();
            foreach (var n in nhomNguoiDung)
            {
                var dv = xlDonVi.Doc(n.IdDonVi);
                if(dv != null)
                {
                    n.TenDonVi = dv.Ten;
                }
            }
            return nhomNguoiDung;
        }
        public bool Xoa(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;
            var nhomNguoiDung = Doc(id);
            if (nhomNguoiDung == null) return false;

            //Nếu nhóm người dùng đang được sử dụng thì ko được phép xóa
            XuLyNguoiDung xlNguoiDung = new XuLyNguoiDung();
            var nd = xlNguoiDung.DocDanhSach(n => n.DanhSachNhom.Contains(id));
            if (nd.Count() > 0) return false;
            return Xoa(nhomNguoiDung);
        }

        public new bool Ghi(NhomNguoiDung entity)
        {
            try
            {
                var xlnd = new XuLyNguoiDung();
                var users = entity.DanhSachNguoiDung.Select(id => xlnd.Doc(id)).ToList();
                foreach (var nguoiDung in users)
                {
                    if (nguoiDung.DanhSachChucNangTheoNhom != null)
                    {
                        if (nguoiDung.DanhSachChucNangTheoCaNhan != null)
                        {
                            nguoiDung.DanhSachChucNang = new List<string>();
                            nguoiDung.DanhSachChucNang.AddRange(nguoiDung.DanhSachChucNangTheoCaNhan);
                            foreach (var s in nguoiDung.DanhSachChucNangTheoNhom.Where(s => !nguoiDung.DanhSachChucNang.Contains(s)))
                            {
                                nguoiDung.DanhSachChucNang.Add(s);
                            }
                        }
                        else
                        {
                            nguoiDung.DanhSachChucNang = nguoiDung.DanhSachChucNangTheoNhom;
                        }
                    }
                    else
                    {
                        if (nguoiDung.DanhSachChucNangTheoCaNhan != null)
                        {
                            nguoiDung.DanhSachChucNang = nguoiDung.DanhSachChucNangTheoCaNhan;
                        }
                    }
                    xlnd.CapNhat(nguoiDung);
                }
                return base.Ghi(entity);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                GC.Collect();
            }
        }

        public new bool Xoa(NhomNguoiDung entity)
        {
            try
            {
                var xlnd = new XuLyNguoiDung();
                var users = entity.DanhSachNguoiDung.Select(id => xlnd.Doc(id)).ToList();
                foreach (var nguoiDung in users)
                {
                    nguoiDung.DanhSachChucNangTheoNhom = null;
                    nguoiDung.DanhSachChucNang = nguoiDung.DanhSachChucNangTheoCaNhan;
                    xlnd.CapNhat(nguoiDung);
                }
                return base.Xoa(entity);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                GC.Collect();
            }
        }

        public new bool CapNhat(NhomNguoiDung entity)
        {
            try
            {
                if (!base.CapNhat(entity)) return false;
                var xlnd = new XuLyNguoiDung();
                var users = entity.DanhSachNguoiDung.Select(id => xlnd.Doc(id)).ToList();
                foreach (var nguoiDung in users)
                {
                    if (nguoiDung.DanhSachChucNangTheoNhom != null)
                    {
                        if (nguoiDung.DanhSachChucNangTheoCaNhan != null)
                        {
                            nguoiDung.DanhSachChucNang = new List<string>();
                            nguoiDung.DanhSachChucNang.AddRange(nguoiDung.DanhSachChucNangTheoCaNhan);
                            foreach (var s in nguoiDung.DanhSachChucNangTheoNhom.Where(s => !nguoiDung.DanhSachChucNang.Contains(s)))
                            {
                                nguoiDung.DanhSachChucNang.Add(s);
                            }
                        }
                        else
                        {
                            nguoiDung.DanhSachChucNang = nguoiDung.DanhSachChucNangTheoNhom;
                        }
                    }
                    else
                    {
                        if (nguoiDung.DanhSachChucNangTheoCaNhan != null)
                        {
                            nguoiDung.DanhSachChucNang = nguoiDung.DanhSachChucNangTheoCaNhan;
                        }
                    }
                    xlnd.CapNhat(nguoiDung);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
