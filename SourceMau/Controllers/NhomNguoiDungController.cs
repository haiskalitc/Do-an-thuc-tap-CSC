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
    public class NhomNguoiDungController : Controller
    {
        private readonly IXuLyNhomNguoiDung xlNhomNguoiDung;
        private readonly IXuLyDonVi xlDonVi;
        private readonly IXuLyChucNang xlChucNang;
        private readonly NguoiDung currentUser;
        public NhomNguoiDungController(IXuLyNhomNguoiDung xlNhomNguoiDung, IXuLyDonVi xlDonVi, IXuLyChucNang xlChucNang)
        {
            this.xlNhomNguoiDung = xlNhomNguoiDung;
            this.xlDonVi = xlDonVi;
            this.xlChucNang = xlChucNang;
            currentUser = SessionManager.ReturnSessionObject(ConstantValues.SessionKeyCurrentUser) as NguoiDung;
        }
        // GET: NhomNguoiDung
        public ActionResult Index(int page = 1, int total = 0)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                long currentRecord = ((page - 1) * PageNumber.NhomNguoiDung);
                var model = new NhomNguoiDungModel();
                model.DanhSachNhomNguoiDung = xlNhomNguoiDung.DocDanhSachCungTenDonVi(PageNumber.NhomNguoiDung, (int)currentRecord);
                if (total > 0)
                {
                    model.TotalPage = total;
                }
                else
                {
                    model.TotalPage = (int)Math.Round((double)xlNhomNguoiDung.TongSoLuong() / PageNumber.NhomNguoiDung);
                }
                model.CurrentPage = page;
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
                var model = new NhomNguoiDungModel();
                model.DanhSachDonVi = xlDonVi.DocDanhSachTuDonViCha(currentUser.IdDonVi, (bool)Session[ConstantValues.SessionKeyVaiTro]);
                model.DanhSachChucNang = DocDanhSachChucNang();
                return View("Them", model);
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
                try
                {
                    if (!string.IsNullOrEmpty(collection["save"].ToString()))
                    {
                        NhomNguoiDung nhomNguoiDung = new NhomNguoiDung();
                        nhomNguoiDung.Ten = collection["ten"].ToString();
                        nhomNguoiDung.MoTa = collection["moTa"].ToString();
                        nhomNguoiDung.IdDonVi = collection["donVi"].ToString();
                        //nguoi dung chi co the phan quyen nhung chuc nang ma ho co
                        var chucNangList = collection["chucNang"].Split(',');
                        nhomNguoiDung.DanhSachChucNang = chucNangList.ToList();
                        nhomNguoiDung.IdNguoiTao = currentUser.Id.ToString();
                        nhomNguoiDung.IdNguoiCapNhat = currentUser.Id.ToString();
                        if (xlNhomNguoiDung.Ghi(nhomNguoiDung))
                        {
                            thongBao.TypeNotify = "alert-success";
                            thongBao.Message = "Thêm thành công";
                        }
                        else
                        {
                            thongBao.TypeNotify = "alert-danger";
                            thongBao.Message = "Thêm thất bại!";
                        }
                    }
                }
                catch (Exception)
                {
                    thongBao.TypeNotify = "alert-danger";
                    thongBao.Message = "Thêm thất bại!";
                }
                ViewBag.ThongBao = thongBao;
                var model = new NhomNguoiDungModel();
                model.DanhSachDonVi = xlDonVi.DocDanhSachTuDonViCha(currentUser.IdDonVi, (bool)Session[ConstantValues.SessionKeyVaiTro]);
                model.DanhSachChucNang = DocDanhSachChucNang();
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
                if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "NhomNguoiDung");
                var model = new NhomNguoiDungModel();
                model.NhomNguoiDungHienTai = xlNhomNguoiDung.Doc(id);
                if (model.NhomNguoiDungHienTai == null) return RedirectToAction("Index", "NhomNguoiDung");
                model.DanhSachDonVi = xlDonVi.DocDanhSachTuDonViCha(currentUser.IdDonVi, (bool)Session[ConstantValues.SessionKeyVaiTro]);
                model.DanhSachChucNang = DocDanhSachChucNang();
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
                if (!string.IsNullOrEmpty(collection["save"].ToString()))
                {
                    string id = collection["nhomNguoiDungId"].ToString();
                    if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "NhomNguoiDung");
                    var model = new NhomNguoiDungModel();
                    NhomNguoiDung nhomNguoiDung = xlNhomNguoiDung.Doc(id);
                    model.NhomNguoiDungHienTai = nhomNguoiDung;
                    if (nhomNguoiDung == null) return RedirectToAction("Index", "NhomNguoiDung");
                    NotifyModel thongBao = new NotifyModel();
                    var user = (NguoiDung)SessionManager.ReturnSessionObject(ConstantValues.SessionKeyCurrentUser);
                    try
                    {
                        nhomNguoiDung.Ten = collection["ten"].ToString();
                        nhomNguoiDung.MoTa = collection["moTa"].ToString();
                        nhomNguoiDung.IdDonVi = collection["donVi"].ToString();
                        //nguoi dung chi co the phan quyen nhung chuc nang ma ho co
                        if (collection["chucNang"] != null)
                        {
                            var chucNangList = collection["chucNang"].Split(',');
                            nhomNguoiDung.DanhSachChucNang = chucNangList.ToList();
                        }
                        else
                        {
                            nhomNguoiDung.DanhSachChucNang = new List<string>();
                        }
                        
                        nhomNguoiDung.IdNguoiCapNhat = user.Id.ToString();
                        if (xlNhomNguoiDung.CapNhat(nhomNguoiDung))
                        {
                            thongBao.TypeNotify = "alert-success";
                            thongBao.Message = "Cập nhật thông tin thành công";
                        }
                        else
                        {
                            thongBao.TypeNotify = "alert-danger";
                            thongBao.Message = "Cập nhật thông tin thất bại!";
                        }
                    }
                    catch (Exception)
                    {
                        thongBao.TypeNotify = "alert-danger";
                        thongBao.Message = "Cập nhật thông tin thất bại!";
                    }

                    ViewBag.ThongBao = thongBao;
                    model.DanhSachDonVi = xlDonVi.DocDanhSachTuDonViCha(user.IdDonVi, (bool)Session[ConstantValues.SessionKeyVaiTro]);
                    model.DanhSachChucNang = DocDanhSachChucNang();
                    return View("ChinhSua", model);
                }
                return RedirectToAction("Index", "NhomNguoiDung");
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
        #region - Support Function
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