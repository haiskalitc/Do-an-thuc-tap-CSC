using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Core.Interface.Service;
using Microsoft.AspNet.SignalR.Hubs;

namespace SystemWebUI.Migrations
{
    [HubName("nhomNguoiDungHub")]
    public class NhomNguoiDungHub : Hub
    {
        private readonly IXuLyNhomNguoiDung xlNhomNguoiDung;
        public NhomNguoiDungHub(IXuLyNhomNguoiDung xlNhomNguoiDung)
        {
            this.xlNhomNguoiDung = xlNhomNguoiDung;
        }
        public void Hello()
        {
            Clients.All.hello();
        }
        public void xoaNhomNguoiDung(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    Clients.Caller.DeletedResult(false);
                }
                else
                {
                    Clients.Caller.DeletedResult(xlNhomNguoiDung.Xoa(id));
                }
            }
            catch (Exception)
            {
                Clients.Caller.DeletedResult(false);
            }

        }
    }
}