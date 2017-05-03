using Common.Enum;
using Common.Utilities;
using Core.Interface.Service;
using Core.Model;
using SystemWebUI.Migrations;
using SystemWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SystemWebUI.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly IXuLyNguoiDung xlNguoiDung;
        private readonly IXuLyVaiTro xlVaiTro;
        private readonly IXuLyDonVi xlDonVi;
        private readonly IXuLyNhomNguoiDung xlNhomNguoiDung;
        private readonly IXuLyChucNang xlChucNang;
        private readonly NguoiDung currentUser;
        public NguoiDungController(IXuLyNguoiDung xlNguoiDung, IXuLyVaiTro xlVaiTro, IXuLyDonVi xlDonVi, IXuLyNhomNguoiDung xlNhomNguoiDung, IXuLyChucNang xlChucNang)
        {
            this.xlNguoiDung = xlNguoiDung;
            this.xlVaiTro = xlVaiTro;
            this.xlDonVi = xlDonVi;
            this.xlNhomNguoiDung = xlNhomNguoiDung;
            this.xlChucNang = xlChucNang;
            currentUser = (NguoiDung)SessionManager.ReturnSessionObject(ConstantValues.SessionKeyCurrentUser);
        }
        // GET: NguoiDung
        public ActionResult Index(int page = 1, int total = 0,string tuKhoa = "", string dieuKienLoc = "0")
        {

            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                long currentRecord = ((page - 1) * PageNumber.NguoiDung);
                var model = new NguoiDungModel();
                string donViId = "";
                if (!dieuKienLoc.Equals("0"))
                {
                    donViId = dieuKienLoc;
                }
                else
                {
                    if ((bool)Session[ConstantValues.SessionKeyVaiTro])
                    {
                        donViId = "0";
                    }
                    else
                    {
                        donViId = currentUser.IdDonVi;
                    }
                }
                
                model.DanhSachNguoiDung = xlNguoiDung.DocDanhSachCungTenDonVi(PageNumber.NguoiDung, (int)currentRecord, tuKhoa, donViId);
                if (total > 0)
                {
                    model.TotalPage = total;
                }
                else
                {
                    model.TotalPage = (int)Math.Round((double)xlNguoiDung.TongSoLuong(tuKhoa,donViId) / PageNumber.NguoiDung);
                }
                model.DanhSachDonVi = xlDonVi.DocDanhSachTuDonViCha(currentUser.IdDonVi, (bool)Session[ConstantValues.SessionKeyVaiTro]);
                model.CurrentPage = page;
                model.TuKhoa = tuKhoa;
                return View("Index", model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult TimKiem(FormCollection collection, int page = 1, int total = 0)
        {

            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                string tuKhoa = collection["tuKhoa"].ToString();
                string dieuKienLoc = collection["dieukienloc"].ToString();

                var model = new NguoiDungModel();
                string donViId = "";
                if (!dieuKienLoc.Equals("0"))
                {
                    donViId = dieuKienLoc;
                }
                else
                {
                    if ((bool)Session[ConstantValues.SessionKeyVaiTro])
                    {
                        donViId = "0";
                    }
                    else
                    {
                        donViId = currentUser.IdDonVi;
                    }
                }
                model.DanhSachNguoiDung = xlNguoiDung.DocDanhSachCungTenDonVi(PageNumber.NguoiDung, 0, tuKhoa, donViId);
                model.DanhSachDonVi = xlDonVi.DocDanhSachTuDonViCha(currentUser.IdDonVi, (bool)Session[ConstantValues.SessionKeyVaiTro]);
                model.CurrentPage = page;
                if (total > 0)
                {
                    model.TotalPage = total;
                }
                else
                {
                    model.TotalPage = (int)Math.Round((double)xlNguoiDung.TongSoLuong(tuKhoa, donViId) / PageNumber.NguoiDung);
                }
                model.TuKhoa = tuKhoa;
                model.DieuKienLoc = donViId;
                return View("Index", model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Them()
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                var model = new NguoiDungModel();
                model.DanhSachVaiTro = DocDanhSachVaiTro();
                model.DanhSachDonVi = DocDanhSachDonVi();
                model.DanhSachNhomNguoiDung = DocDanhSachNhomNguoiDung();
                model.NguoiDungHienTai = new NguoiDung();
                return View("Them",model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]
        public ActionResult Them(FormCollection collection)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                NotifyModel thongBao = new NotifyModel();
                NguoiDung nguoiDung = new NguoiDung();
                try
                {
                    if (!string.IsNullOrEmpty(collection["save"].ToString()))
                    {
                        string tenDangNhap = collection["tenDangNhap"].ToString();
                        int kichHoat = 0;
                        if (collection["kich_hoat"] != null && !string.IsNullOrEmpty(collection["kich_hoat"].ToString()) && collection["kich_hoat"].ToString() == "on")
                        {
                            kichHoat = 1;
                        }
                        nguoiDung.Ten = collection["ten"].ToString();
                        nguoiDung.TenDangNhap = tenDangNhap;
                        nguoiDung.setMatKhau(collection["matKhau"].ToString());

                        nguoiDung.Email = collection["email"].ToString();
                        nguoiDung.DienThoai = collection["dienThoai"].ToString();

                        nguoiDung.IdVaiTro = collection["vaiTro"].ToString();
                        nguoiDung.IdDonVi = collection["donVi"].ToString();
                        string nhomNguoiDungIds = collection["nhomNguoiDung"].ToString();
                        List<string> nndIdList = nhomNguoiDungIds.Split(',').ToList();
                        nguoiDung.DanhSachNhom = nndIdList;
                        List<string> dsChucNang = new List<string>();
                        foreach (var nndId in nndIdList)
                        {
                            NhomNguoiDung nhomNguoiDung = xlNhomNguoiDung.Doc(nndId);
                            foreach (var cn in nhomNguoiDung.DanhSachChucNang)
                            {
                                if (!dsChucNang.Contains(cn))
                                {
                                    dsChucNang.Add(cn);
                                }
                            }
                        }
                        nguoiDung.DanhSachChucNang = dsChucNang;
                        nguoiDung.KichHoat = kichHoat;
                        nguoiDung.IdNguoiTao = currentUser.Id.ToString();
                        nguoiDung.IdNguoiCapNhat = currentUser.Id.ToString();
                        var nguoiDungExists = xlNguoiDung.DocDanhSach(nd => nd.TenDangNhap == tenDangNhap);
                        if (nguoiDungExists.Count() > 0)
                        {
                            thongBao.TypeNotify = "alert-danger";
                            thongBao.Message = "Tên người dùng đã tồn tại! Thêm thất bại!";
                        }
                        else
                        {
                            if (xlNguoiDung.Ghi(nguoiDung))
                            {
                                thongBao.TypeNotify = "alert-success";
                                thongBao.Message = "Thêm thành công";
                                nguoiDung = new NguoiDung();
                            }
                            else
                            {
                                thongBao.TypeNotify = "alert-danger";
                                thongBao.Message = "Thêm thất bại!";
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    thongBao.TypeNotify = "alert-danger";
                    thongBao.Message = "Thêm thất bại!";
                }
                ViewBag.ThongBao = thongBao;
                var model = new NguoiDungModel();
                model.DanhSachVaiTro = DocDanhSachVaiTro();
                model.DanhSachDonVi = DocDanhSachDonVi();
                model.DanhSachNhomNguoiDung = DocDanhSachNhomNguoiDung();
                model.NguoiDungHienTai = nguoiDung; 
                return View("Them", model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }

        public ActionResult ChinhSua(string id)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "NguoiDung");
                var model = new NguoiDungModel();
                model.NguoiDungHienTai = xlNguoiDung.Doc(id);
                if (model.NguoiDungHienTai == null) return RedirectToAction("Index", "NguoiDung");
                model.DanhSachVaiTro = DocDanhSachVaiTro();
                model.DanhSachDonVi = DocDanhSachDonVi();
                model.DanhSachNhomNguoiDung = DocDanhSachNhomNguoiDung();
                return View("ChinhSua", model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]
        public ActionResult ChinhSua(FormCollection collection)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                NotifyModel thongBao = new NotifyModel();
                string id = collection["nguoiDungId"].ToString();
                if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "NguoiDung");
                NguoiDung nguoiDung = xlNguoiDung.Doc(id);
                try
                {
                    if (!string.IsNullOrEmpty(collection["save"].ToString()))
                    {
                        int kichHoat = 0;
                        if (collection["kich_hoat"] != null && !string.IsNullOrEmpty(collection["kich_hoat"].ToString()) && collection["kich_hoat"].ToString() == "on")
                        {
                            kichHoat = 1;
                        }
                        nguoiDung.Ten = collection["ten"].ToString();
                        nguoiDung.Email = collection["email"].ToString();
                        nguoiDung.DienThoai = collection["dienThoai"].ToString();
                        nguoiDung.IdVaiTro = collection["vaiTro"].ToString();
                        nguoiDung.IdDonVi = collection["donVi"].ToString();
                        var nhomNguoiDungIds = collection["nhomNguoiDung"].Split(',');
                        List<string> nndIdList = nhomNguoiDungIds.ToList();
                        nguoiDung.DanhSachNhom = nndIdList;
                        List<string> dsChucNang = nguoiDung.DanhSachChucNang;
                        foreach (var nndId in nndIdList)
                        {
                            NhomNguoiDung nhomNguoiDung = xlNhomNguoiDung.Doc(nndId);
                            foreach (var cn in nhomNguoiDung.DanhSachChucNang)
                            {
                                if (!dsChucNang.Contains(cn))
                                {
                                    dsChucNang.Add(cn);
                                }
                            }
                        }
                        nguoiDung.DanhSachChucNang = dsChucNang;
                        nguoiDung.KichHoat = kichHoat;
                        nguoiDung.IdNguoiCapNhat = currentUser.Id.ToString();
                        if (xlNguoiDung.CapNhat(nguoiDung))
                        {
                            thongBao.TypeNotify = "alert-success";
                            thongBao.Message = "Chỉnh sửa thông tin thành công";
                        }
                        else
                        {
                            thongBao.TypeNotify = "alert-danger";
                            thongBao.Message = "Chỉnh sửa thông tin thất bại!";
                        }
                        
                    }
                }
                catch (Exception)
                {
                    thongBao.TypeNotify = "alert-danger";
                    thongBao.Message = "Chỉnh sửa thông tin thất bại!";
                }
                ViewBag.ThongBao = thongBao;
                var model = new NguoiDungModel();
                model.DanhSachVaiTro = DocDanhSachVaiTro();
                model.DanhSachDonVi = DocDanhSachDonVi();
                model.DanhSachNhomNguoiDung = DocDanhSachNhomNguoiDung();
                model.NguoiDungHienTai = nguoiDung;
                return View("ChinhSua", model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
        
        public ActionResult PhanQuyen(string id)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "NguoiDung");
                var model = new NguoiDungModel();
                model.NguoiDungHienTai = xlNguoiDung.Doc(id);
                if (model.NguoiDungHienTai == null) return RedirectToAction("Index", "NguoiDung");
                model.DanhSachChucNang = DocDanhSachChucNang();
                return View("PhanQuyen", model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]
        public ActionResult PhanQuyen(FormCollection collection)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                NotifyModel thongBao = new NotifyModel();
                string id = collection["nguoiDungId"].ToString();
                if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "NguoiDung");
                NguoiDung nguoiDung = xlNguoiDung.Doc(id);
                try
                {
                    if (!string.IsNullOrEmpty(collection["save"].ToString()))
                    {
                        var chucNangList = collection["chucNang"].Split(',');
                        nguoiDung.DanhSachChucNang = chucNangList.ToList();
                        nguoiDung.IdNguoiCapNhat = currentUser.Id.ToString();
                        if (xlNguoiDung.CapNhat(nguoiDung))
                        {
                            thongBao.TypeNotify = "alert-success";
                            thongBao.Message = "Cập nhật phân quyền thành công";
                        }
                        else
                        {
                            thongBao.TypeNotify = "alert-danger";
                            thongBao.Message = "Cập nhật phân quyền thất bại!";
                        }

                    }
                }
                catch (Exception)
                {
                    thongBao.TypeNotify = "alert-danger";
                    thongBao.Message = "Cập nhật phân quyền thất bại!";
                }
                ViewBag.ThongBao = thongBao;
                var model = new NguoiDungModel();
                model.DanhSachChucNang = DocDanhSachChucNang();
                model.NguoiDungHienTai = nguoiDung;
                return View("PhanQuyen", model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
        public ActionResult ThongTinCaNhan()
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                var model = new NguoiDungModel();
                model.NguoiDungHienTai = currentUser;
                return View("ThongTinCaNhan", model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]
        public ActionResult ThongTinCaNhan(FormCollection collection)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                NotifyModel thongBao = new NotifyModel();
                NguoiDung nguoiDung = xlNguoiDung.Doc(currentUser.Id.ToString());
                try
                {
                    if (!string.IsNullOrEmpty(collection["save"].ToString()))
                    {
                        nguoiDung.Ten = collection["ten"].ToString();
                        nguoiDung.Email = collection["email"].ToString();
                        nguoiDung.DienThoai = collection["dienThoai"].ToString();
                        nguoiDung.IdNguoiCapNhat = currentUser.Id.ToString();
                        if (xlNguoiDung.CapNhat(nguoiDung))
                        {
                            thongBao.TypeNotify = "alert-success";
                            thongBao.Message = "Chỉnh sửa thông tin thành công";
                            SessionManager.RegisterSession(ConstantValues.SessionKeyCurrentUser, nguoiDung);
                        }
                        else
                        {
                            thongBao.TypeNotify = "alert-danger";
                            thongBao.Message = "Chỉnh sửa thông tin thất bại!";
                        }
                    }
                }
                catch (Exception)
                {
                    thongBao.TypeNotify = "alert-danger";
                    thongBao.Message = "Chỉnh sửa thông tin thất bại!";
                }
                ViewBag.ThongBao = thongBao;
                var model = new NguoiDungModel();
                model.NguoiDungHienTai = nguoiDung;
                return View("ThongTinCaNhan", model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
        #region - Support Functions
        private List<NhomNguoiDung> DocDanhSachNhomNguoiDung()
        {
            if ((bool)Session[ConstantValues.SessionKeyVaiTro])
            {
                return xlNhomNguoiDung.DocDanhSach().ToList();
            }
            else
            {
                return xlNhomNguoiDung.DocDanhSach(n => n.IdDonVi == currentUser.IdDonVi).ToList();
            }
            
        }
        private List<DonVi> DocDanhSachDonVi()
        {
            return xlDonVi.DocDanhSachTuDonViCha(currentUser.IdDonVi, (bool)Session[ConstantValues.SessionKeyVaiTro]);
        }
        private List<VaiTro> DocDanhSachVaiTro()
        {
            var vaiTroList = xlVaiTro.DocDanhSach().ToList();
            if ((bool)Session[ConstantValues.SessionKeyVaiTro])
            {
                return vaiTroList;
            }
            else
            {
                var vaiTro = vaiTroList.Find(v => v.Id.ToString() == ConstantValues.VaiTroQuanTriHeThong);
                if (vaiTro != null) vaiTroList.Remove(vaiTro);
                return vaiTroList;
            }
        }
        private List<MenuQuyenHanModel> DocDanhSachChucNang()
        {
            if ((bool)Session[ConstantValues.SessionKeyVaiTro])
            {
                return MenuQuyenHanModel.SapXepMenu(xlChucNang.DocDanhSachChucNang().ToList());
            }
            else
            {
                return MenuQuyenHanModel.SapXepMenu(xlChucNang.DocDanhSachTatCaChucNangTuDanhSachId(currentUser.DanhSachChucNang));
            }
        }
        #endregion
    }

}