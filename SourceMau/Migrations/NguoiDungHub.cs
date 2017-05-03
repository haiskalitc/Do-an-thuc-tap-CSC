using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Core.Interface.Service;
using Core.Model;
using Common.Utilities;

namespace SystemWebUI.Migrations
{
    [HubName("nguoiDungHub")]
    public class NguoiDungHub : Hub
    {
        private readonly IXuLyNguoiDung xlNguoiDung;
        public NguoiDungHub(IXuLyNguoiDung xlNguoiDung)
        {
            this.xlNguoiDung = xlNguoiDung;
        }

        public void Hello()
        {
            Clients.All.hello();
        }
        public void kichHoatTaiKhoan(string id, string userId, int trangThai)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) Clients.Caller.UpdateResult(false);
                NguoiDung nd = xlNguoiDung.Doc(id);
                if(nd==null) Clients.Caller.UpdateResult(false);
                nd.KichHoat = trangThai;
                nd.IdNguoiCapNhat = userId;
                Clients.Caller.UpdateResult(xlNguoiDung.CapNhat(nd));
            }
            catch (Exception)
            {
                Clients.Caller.UpdateResult(false);
            }

        }
        public void capNhatMatKhau(string id, string userId, string matKhau, string matKhauCu ="")
        {
            try
            {
                if (string.IsNullOrEmpty(id)) Clients.Caller.UpdateResult(false);
                NguoiDung nd = xlNguoiDung.Doc(id);
                if (nd == null) Clients.Caller.UpdateResult(false);
                if (matKhauCu !="")
                {
                    if(nd.MatKhau != Utility.MD5(matKhauCu))
                    {
                        Clients.Caller.WrongPassword();
                        return;
                    }
                }
                nd.setMatKhau(matKhau);
                nd.IdNguoiCapNhat = userId;
                Clients.Caller.UpdateResult(xlNguoiDung.CapNhat(nd));
            }
            catch (Exception)
            {
                Clients.Caller.UpdateResult(false);
            }
        }
    }
    
}