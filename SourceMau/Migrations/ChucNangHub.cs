using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Core.Interface.Service;
using Core.Model;
using Microsoft.AspNet.SignalR.Hubs;

namespace SystemWebUI.Migrations
{
    [HubName("chucNangHub")]
    public class ChucNangHub : Hub
    {
        private readonly IXuLyChucNang xlChucNang;
        public ChucNangHub(IXuLyChucNang xlChucNang)
        {
            this.xlChucNang = xlChucNang;
        }
        public void Hello()
        {
            Clients.All.hello();
        }
        public void batTatChucNang(string id, int trangthai)
        {
            try
            {
                ChucNang chucNang = xlChucNang.Doc(id);
                if (chucNang != null)
                {
                    chucNang.KichHoat = trangthai;
                    Clients.Caller.UpdateResult(xlChucNang.CapNhat(chucNang));
                }
                else
                {
                    Clients.Caller.UpdateResult(false);
                }
            }catch(Exception)
            {
                Clients.Caller.UpdateResult(false);
            }
            
        }
        public void xoaChucNang(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    Clients.Caller.DeletedResult(false);
                }
                else
                {
                    Clients.Caller.DeletedResult(xlChucNang.Xoa(id));
                }
            }catch(Exception)
            {
                Clients.Caller.DeletedResult(false);
            }
            
        }
    }
}