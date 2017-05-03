using Common.Enum;
using Common.Utilities;
using Core.Interface;
using Core.Interface.Service;
using SystemWebUI.Migrations;
using SystemWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SystemWebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IXuLyNguoiDung _xlNguoiDung;
        public LoginController(IXuLyNguoiDung xlNguoiDung)
        {
            _xlNguoiDung = xlNguoiDung;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //[CaptchaValidation("CaptchaCode", "exampleCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _xlNguoiDung.DangNhap(model.UserName.ToLower(), Utility.MD5(model.Password));
                if (user != null)
                {
                    //Session["loginCount"] = 0;
                    SessionManager.RegisterSession(ConstantValues.SessionKeyUserId, user.Id);
                    //SessionManager.RegisterSession("quyenHan", user.DanhSachChucNang);
                    SessionManager.RegisterSession(ConstantValues.SessionKeyCurrentUser, user);
                    //kiểm tra xem user co phải thuộc tài khoản quản trị hệ thống ko
                    SessionManager.RegisterSession(ConstantValues.SessionKeyVaiTro, user.IdVaiTro == ConstantValues.VaiTroQuanTriHeThong);
                  
                    if (SessionManager.CheckSession(ConstantValues.SessionKeyUrl))
                        return Redirect(SessionManager.ReturnSessionObject(ConstantValues.SessionKeyUrl).ToString());
                    return RedirectToAction("Index", "Home");
                }
                model.Result = false;
                model.Password = null;
                //if (Session["loginCount"] == null)
                //{
                //    SessionManager.RegisterSession("loginCount", 0);
                //}
            }
            return View("Index",model);
        }
        public ActionResult Logout()
        {
            if (SessionManager.CheckSession(ConstantValues.SessionKeyCurrentUser))
            {
                SessionManager.FreeSession(ConstantValues.SessionKeyCurrentUser);
                SessionManager.FreeSession(ConstantValues.SessionKeyUrl);
                SessionManager.FreeSession(ConstantValues.SessionKeyUserId);
                SessionManager.FreeSession(ConstantValues.SessionKeyMenu);
                SessionManager.FreeSession(ConstantValues.SessionKeyVaiTro);
                return RedirectToAction("Index", "Login");
            }
            if (Request.Url != null)
                SessionManager.RegisterSession(ConstantValues.SessionKeyUrl, Request.Url.AbsolutePath);
            return RedirectToAction("Index", "Login");
        }
    }
}