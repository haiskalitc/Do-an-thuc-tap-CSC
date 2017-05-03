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
    public class DeveloperController : Controller
    {
     
        private readonly IXuLyChucNang xlchucnang;
        public DeveloperController(IXuLyChucNang xlchucnang)
        {
            this.xlchucnang = xlchucnang;
        }
        // GET: Developer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachChucNang()
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                var chucNangs = xlchucnang.DocDanhSachTatCaChucNang();
                List<MenuQuyenHanModel> menuModel = MenuQuyenHanModel.SapXepMenu(chucNangs);
                return View("DanhSachChucNang", menuModel);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");

        }
        public ActionResult ThemChucNang()
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                var chucNang = xlchucnang.DocDanhSachChucNangCapCha();
                return View("ThemChucNang", chucNang);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]
        public ActionResult ThemChucNang(FormCollection collection)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                NotifyModel thongBao = new NotifyModel();
                var dsChucNang = xlchucnang.DocDanhSachChucNangCapCha();
                try
                {
                    if (!string.IsNullOrEmpty(collection["save"].ToString()))
                    {
                        var user = (NguoiDung)SessionManager.ReturnSessionObject(ConstantValues.SessionKeyCurrentUser);
                        ChucNang chucNang = new ChucNang();
                        int kichHoat = 0;
                        if (collection["kich_hoat"] != null && !string.IsNullOrEmpty(collection["kich_hoat"].ToString()) && collection["kich_hoat"].ToString() == "on")
                        {
                            kichHoat = 1;
                        }
                        chucNang.Ten = collection["ten"].ToString();
                        chucNang.BieuTuong = collection["bieu_tuong"].ToString();
                        chucNang.LienKet = collection["lien_ket"].ToString();
                        chucNang.IdCha = collection["chuc_nang_cha"].ToString();
                        if (chucNang.IdCha != "0")
                        {
                            //hiện tại chỉ hỗ trợ cho 2 cấp thôi nên nếu có cha thì cấp con sẽ là 2
                            chucNang.CapDo = 2;
                        }
                        chucNang.NgayCapNhat = Utility.ConvertToUnixTimestamp(DateTime.UtcNow);
                        chucNang.IdNguoiTao = user.Id.ToString();
                        chucNang.IdNguoiCapNhat = user.Id.ToString();
                        chucNang.SapXep = int.Parse(collection["sap_xep"].ToString());
                        chucNang.KichHoat = kichHoat;

                        if (xlchucnang.Ghi(chucNang))
                        {
                            thongBao.TypeNotify = "alert-success";
                            thongBao.Message = "Thêm thành công";
                            if (chucNang.CapDo == 1)
                            {
                                dsChucNang.Add(chucNang);
                            }
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
                return View("ThemChucNang", dsChucNang);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
        public ActionResult ChinhSuaChucNang(string id)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                if(string.IsNullOrEmpty(id)) return RedirectToAction("DanhSachChucNang", "Developer");
                var model = new ChucNangModel();
                model.ChucNangObj = xlchucnang.Doc(id);
                if(model.ChucNangObj == null) return RedirectToAction("DanhSachChucNang", "Developer");
                model.DanhSachChucNang = xlchucnang.DocDanhSachChucNangCapCha();
                var cn = model.DanhSachChucNang.Find(c => c.Id.ToString() == id);
                model.DanhSachChucNang.Remove(cn);
                return View("ChinhSuaChucNang", model);
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]
        public ActionResult ChinhSuaChucNang(FormCollection collection)
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                if (!string.IsNullOrEmpty(collection["save"].ToString()))
                {
                    string id = collection["chucNangId"].ToString();
                    if(string.IsNullOrEmpty(id)) return RedirectToAction("DanhSachChucNang", "Developer");
                    ChucNang chucNang = xlchucnang.Doc(id);
                    if(chucNang == null) return RedirectToAction("DanhSachChucNang", "Developer");
                    NotifyModel thongBao = new NotifyModel();
                    var dsChucNang = xlchucnang.DocDanhSachChucNangCapCha();
                    try
                    {
                        var user = (NguoiDung)SessionManager.ReturnSessionObject(ConstantValues.SessionKeyCurrentUser);
                        
                        int kichHoat = 0;
                        if (collection["kich_hoat"] != null && !string.IsNullOrEmpty(collection["kich_hoat"].ToString()) && collection["kich_hoat"].ToString() == "on")
                        {
                            kichHoat = 1;
                        }
                        chucNang.Ten = collection["ten"].ToString();
                        chucNang.BieuTuong = collection["bieu_tuong"].ToString();
                        chucNang.LienKet = collection["lien_ket"].ToString();
                        chucNang.IdCha = collection["chuc_nang_cha"].ToString();
                        if (chucNang.IdCha != "0")
                        {
                            //hiện tại chỉ hỗ trợ cho 2 cấp thôi nên nếu có cha thì cấp con sẽ là 2
                            chucNang.CapDo = 2;
                        }
                        else
                        {
                            chucNang.CapDo = 1;
                        }
                        chucNang.NgayCapNhat = Utility.ConvertToUnixTimestamp(DateTime.UtcNow);
                        chucNang.IdNguoiCapNhat = user.Id.ToString();
                        chucNang.SapXep = int.Parse(collection["sap_xep"].ToString());
                        chucNang.KichHoat = kichHoat;

                        if (xlchucnang.CapNhat(chucNang))
                        {
                            thongBao.TypeNotify = "alert-success";
                            thongBao.Message = "Cập nhật thành công";
                        }
                        else
                        {
                            thongBao.TypeNotify = "alert-danger";
                            thongBao.Message = "Cập nhật thất bại!";
                        }
                    }
                    catch (Exception)
                    {
                        thongBao.TypeNotify = "alert-danger";
                        thongBao.Message = "Cập nhật thất bại!";
                    }
                    ViewBag.ThongBao = thongBao;
                    var model = new ChucNangModel();
                    model.ChucNangObj = chucNang;
                    model.DanhSachChucNang = dsChucNang;
                    return View("ChinhSuaChucNang", model);
                }
                return RedirectToAction("DanhSachChucNang", "Developer");
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
    }
}