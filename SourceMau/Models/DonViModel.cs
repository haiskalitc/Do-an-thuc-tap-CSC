using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemWebUI.Models
{
    public class DonViModel
    {
        public List<DonVi> DanhSachDonVi;
        public int TotalPage;
        public int CurrentPage;
        public DonVi DonViHienTai;
    }
}