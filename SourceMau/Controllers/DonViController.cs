using Common.Enum;
using Common.Utilities;
using Core.Interface.Service;
using Core.Model;
using SystemWebUI.Migrations;
using SystemWebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SystemWebUI.Controllers
{
    public class DonViController : Controller
    {
        private readonly IXuLyDonVi xlDonVi;
        private readonly NguoiDung currentUser;
        public DonViController(IXuLyDonVi xlDonVi)
        {
            this.xlDonVi = xlDonVi;
            currentUser = (NguoiDung)SessionManager.ReturnSessionObject(ConstantValues.SessionKeyCurrentUser);
        }
        // GET: DonVi
        public ActionResult Index(int page = 1, int total = 0)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                long currentRecord = ((page - 1) * PageNumber.DonVi);
                var model = new DonViModel();
                model.DanhSachDonVi = xlDonVi.DocDanhSachTuDonViCha(currentUser.IdDonVi,PageNumber.DonVi, (int)currentRecord, (bool)Session[ConstantValues.SessionKeyVaiTro]);
                if(total > 0)
                {
                    model.TotalPage = total;
                }
                else
                {
                    model.TotalPage =  (int)Math.Round( (double)xlDonVi.TongSoLuong()/PageNumber.DonVi);
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
                var model = xlDonVi.DocDanhSachTuDonViCha(currentUser.IdDonVi, (bool)Session[ConstantValues.SessionKeyVaiTro]);
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
                try
                {
                    if (!string.IsNullOrEmpty(collection["save"].ToString()))
                    {
                        var user = (NguoiDung)SessionManager.ReturnSessionObject(ConstantValues.SessionKeyCurrentUser);
                        DonVi donVi = new DonVi();
                        donVi.Ten = collection["ten"].ToString();
                        donVi.DiaChi = collection["diaChi"].ToString();
                        donVi.DienThoai = collection["dienThoai"].ToString();
                        donVi.Email = collection["email"].ToString();
                        donVi.Fax = collection["fax"].ToString();
                        //donVi.HoTenLanhDao = collection["hoTenLanhDao"].ToString();
                        //donVi.DienThoaiLanhDao = collection["dienThoaiLanhDao"].ToString();
                        //donVi.EmailLanhDao = collection["emailLanhDao"].ToString();
                        if (collection["donViTrucThuoc"].ToString() == "0")
                        {
                            donVi.IdDonViTrucThuoc = "0";
                            donVi.Cap = 1;
                        }
                        else
                        {
                            var dvTrucThuoc = xlDonVi.Doc(collection["donViTrucThuoc"].ToString());
                            donVi.IdDonViTrucThuoc = dvTrucThuoc.Id.ToString();
                            donVi.Cap = dvTrucThuoc.Cap + 1;
                        }
                        donVi.IdNguoiTao = user.Id.ToString();
                        donVi.IdNguoiCapNhat = user.Id.ToString();
                        HttpPostedFileBase file = Request.Files["logoDonVi"];
                        if (file.FileName != null && file.FileName != "")
                        {
                            if (file.ContentLength > 0)
                            {
                                string savedFileName = "";
                                string savedFilePath = UploadFile.getFullFilePath(UploadFile.DonViDirectory, file.FileName, out savedFileName);
                                file.SaveAs(savedFilePath);
                                donVi.Logo = "/" + UploadFile.DonViDirectory + savedFileName;
                            }
                        }
                        if (xlDonVi.Ghi(donVi))
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
                var model = xlDonVi.DocDanhSachTuDonViCha(currentUser.IdDonVi, (bool)Session[ConstantValues.SessionKeyVaiTro]);
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
                if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "DonVi");
                var model = new DonViModel();
                 model.DonViHienTai = xlDonVi.Doc(id);
                model.DanhSachDonVi = xlDonVi.DocDanhSachTuDonViCha(currentUser.IdDonVi, (bool)Session[ConstantValues.SessionKeyVaiTro]);
                if (model == null) return RedirectToAction("Index", "DonVi");
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
                    string id = collection["donViId"].ToString();
                    if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "DonVi");
                    var model = new DonViModel();
                    DonVi donVi = xlDonVi.Doc(id);
                    model.DonViHienTai = donVi;
                    if (donVi == null) return RedirectToAction("Index", "DonVi");
                    NotifyModel thongBao = new NotifyModel();
                    try
                    {
                        var user = (NguoiDung)SessionManager.ReturnSessionObject(ConstantValues.SessionKeyCurrentUser);
                        donVi.Ten = collection["ten"].ToString();
                        donVi.DiaChi = collection["diaChi"].ToString();
                        donVi.DienThoai = collection["dienThoai"].ToString();
                        donVi.Email = collection["email"].ToString();
                        donVi.Fax = collection["fax"].ToString();
                        //donVi.HoTenLanhDao = collection["hoTenLanhDao"].ToString();
                        //donVi.DienThoaiLanhDao = collection["dienThoaiLanhDao"].ToString();
                        //donVi.EmailLanhDao = collection["emailLanhDao"].ToString();
                        if (collection["donViTrucThuoc"].ToString() == "0")
                        {
                            donVi.IdDonViTrucThuoc = "0";
                            donVi.Cap = 1;
                        }
                        else
                        {
                            var dvTrucThuoc = xlDonVi.Doc(collection["donViTrucThuoc"].ToString());
                            donVi.IdDonViTrucThuoc = dvTrucThuoc.Id.ToString();
                            donVi.Cap = dvTrucThuoc.Cap + 1;
                        }
                        donVi.IdNguoiCapNhat = user.Id.ToString();
                        HttpPostedFileBase file = Request.Files["logoDonVi"];
                        if (file.FileName != null && file.FileName != "")
                        {
                            if (file.ContentLength > 0)
                            {
                                string savedFileName = "";
                                string savedFilePath = UploadFile.getFullFilePath(UploadFile.DonViDirectory, file.FileName, out savedFileName);
                                file.SaveAs(savedFilePath);
                                donVi.Logo = "/" + UploadFile.DonViDirectory + savedFileName; 
                            }
                        }
                        if (xlDonVi.CapNhat(donVi))
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
                    model.DanhSachDonVi = xlDonVi.DocDanhSachTuDonViCha(currentUser.IdDonVi, (bool)Session[ConstantValues.SessionKeyVaiTro]);
                    return View("ChinhSua", model);
                }
                return RedirectToAction("Index", "DonVi");
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
        public ActionResult CauHinhMail(string id)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "DonVi");
                var model = xlDonVi.Doc(id);
                if (model == null) return RedirectToAction("Index", "DonVi");
                return View("CauHinhMail", model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]
        public ActionResult CauHinhMail(FormCollection collection)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                if (!string.IsNullOrEmpty(collection["save"].ToString()))
                {
                    string id = collection["donViId"].ToString();
                    if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "DonVi");
                    var model = new DonViModel();
                    DonVi donVi = xlDonVi.Doc(id);
                    if (donVi == null) return RedirectToAction("Index", "DonVi");
                    NotifyModel thongBao = new NotifyModel();
                    try
                    {
                        var user = (NguoiDung)SessionManager.ReturnSessionObject(ConstantValues.SessionKeyCurrentUser);
                        donVi.CauHinhEmail.SMTPServer = collection["smtpServer"].ToString();
                        donVi.CauHinhEmail.Port = int.Parse(collection["port"].ToString());
                        int ssl = 0;
                        if (collection["ssl"] != null && !string.IsNullOrEmpty(collection["ssl"].ToString()) && collection["ssl"].ToString() == "on")
                        {
                            ssl = 1;
                        }
                        donVi.CauHinhEmail.SSL = ssl;
                        donVi.CauHinhEmail.TaiKhoan = collection["taiKhoan"].ToString();
                        donVi.CauHinhEmail.MatKhau = collection["matKhau"].ToString();
                        donVi.CauHinhEmail.TenHienThi = collection["tenHienThi"].ToString();
                        donVi.CauHinhEmail.EmailGui = collection["emailGui"].ToString();
                        donVi.IdNguoiCapNhat = user.Id.ToString();
                        if (xlDonVi.CapNhat(donVi))
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
                    return View("CauHinhMail", donVi);
                }
                return RedirectToAction("Index", "DonVi");
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
    }
}