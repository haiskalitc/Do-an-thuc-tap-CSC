using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Core.Interface.Service;
using Core.Model;
using Common.Enum;

namespace SystemWebUI.Migrations
{
    [HubName("donViHub")]
    public class DonViHub : Hub
    {
        private readonly IXuLyDonVi xlDonVi;
        public DonViHub(IXuLyDonVi xlDonVi)
        {
            this.xlDonVi = xlDonVi;
        }

        public void Hello()
        {
            Clients.All.hello();
        }
        public void xoaDonVi(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    Clients.Caller.DeletedResult(false);
                }
                else
                {
                    Clients.Caller.DeletedResult(xlDonVi.Xoa(id));
                }
            }
            catch (Exception)
            {
                Clients.Caller.DeletedResult(false);
            }

        }
        public void xoaCauHinhEmail(string id, string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    Clients.Caller.DeletedResult(false);
                }
                else
                {
                    var dv = xlDonVi.Doc(id);
                    dv.CauHinhEmail = new CauHinhEmail();
                    dv.IdNguoiCapNhat = userId;
                    Clients.Caller.DeletedResult(xlDonVi.CapNhat(dv));
                }
            }
            catch (Exception)
            {
                Clients.Caller.DeletedResult(false);
            }

        }
    }
}