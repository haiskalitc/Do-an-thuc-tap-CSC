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
    public class QuyenHanChucNangController : Controller
    {
        private readonly IXuLyChucNang xlchucnang;
        public QuyenHanChucNangController(IXuLyChucNang xlchucnang)
        {
            this.xlchucnang = xlchucnang;
        }
        
        // GET: QuyenHanChucNang
        [ChildActionOnly]
        public PartialViewResult MenuQuyenHan()
        {
            List<MenuQuyenHanModel> menuModel = new List<MenuQuyenHanModel>();
            if (Session[ConstantValues.SessionKeyMenu] != null)
            {
                menuModel = new List<MenuQuyenHanModel>(SessionManager.ReturnSessionObject(ConstantValues.SessionKeyMenu) as List<MenuQuyenHanModel>);
            }
            else 
            {
                var user = SessionManager.ReturnSessionObject(ConstantValues.SessionKeyCurrentUser) as NguoiDung;
                if (user.DanhSachChucNang.Count() > 0)
                {
                    //var dsPhanQuyen = user.DanhSachChucNang;  
                    var dsChucNang = xlchucnang.DocDanhSachTatCaChucNangTuDanhSachId(user.DanhSachChucNang);
                    menuModel = MenuQuyenHanModel.SapXepMenu(dsChucNang);
                }
                SessionManager.RegisterSession(ConstantValues.SessionKeyMenu, menuModel);
            }
            
            return PartialView(menuModel);
        }
    }
}