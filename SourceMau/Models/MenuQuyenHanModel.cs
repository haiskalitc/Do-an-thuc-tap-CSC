using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemWebUI.Models
{
    public class MenuQuyenHanModel
    {
        public ChucNang ChucNangChinh;
        public List<ChucNang> DanhSachChucNangCon;
        public MenuQuyenHanModel()
        {
            DanhSachChucNangCon = new List<ChucNang>();
        }
        public static List<MenuQuyenHanModel> SapXepMenu(List<ChucNang> dsChucNang)
        {
            List<MenuQuyenHanModel> menuModel = new List<MenuQuyenHanModel>();
            while (dsChucNang.Count > 0)
            {
                var chucNang = dsChucNang[0];

                if (chucNang.CapDo == 1)
                {
                    MenuQuyenHanModel menuItem = new MenuQuyenHanModel();
                    menuItem.ChucNangChinh = chucNang;
                    dsChucNang.RemoveAt(0);
                    menuModel.Add(menuItem);
                }
                else
                {
                    var menuItem = menuModel.Find(m => m.ChucNangChinh.Id.ToString() == chucNang.IdCha);
                    if (menuItem == null)
                    {
                        menuItem = new MenuQuyenHanModel();
                        menuItem.ChucNangChinh = dsChucNang.Find(c => c.Id.ToString() == chucNang.IdCha);
                        menuModel.Add(menuItem);
                        dsChucNang.Remove(menuItem.ChucNangChinh);
                    }
                    menuItem.DanhSachChucNangCon.Add(chucNang);
                    dsChucNang.RemoveAt(0);
                }
            }
            return menuModel;
        }
    }
}