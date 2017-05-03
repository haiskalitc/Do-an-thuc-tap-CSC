using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemWebUI.Models
{
    public class MenuCha
    {
        public Object ID { get; set; }
        public int IDCha { get; set;}
        public string TieuDe { get; set; }
        public MenuCha(string id,int idcha,string Tieude)
        {
            this.ID = id;
            this.IDCha = idcha;
            this.TieuDe = Tieude;
        }
    }
}