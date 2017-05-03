using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemWebUI.Models
{
    public class MenuCon
    {
        public Object ID { get; set; }

        public Object IDCha { get; set; }

        public string TieuDe { get; set; }
        public string DuongDan { get; set; }
        public MenuCon(string id,string idcha,string tieude,string duongdan)
        {
            this.ID = id;
            this.IDCha = idcha;
            this.TieuDe = tieude;
            this.DuongDan = duongdan;
        }
    }
}