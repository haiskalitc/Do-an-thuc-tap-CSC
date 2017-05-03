using System.Configuration;

namespace SystemWebUI.Migrations
{
    public class AppSettings
    {
        public static int PageSize => int.Parse(ConfigurationManager.AppSettings["PageSize"]);
    }
}