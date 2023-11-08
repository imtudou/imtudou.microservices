using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Web
{
    public class AppsettingsOptions
    {
        public string test { get; set; }
        public string secret { get; set; }
    }

    public class ConnectionStringsOptions
    {
        public string imtudou_saas_userinfodb { get; set; }
        public string mysql { get; set; }
        public string sqlserver { get; set; }
        public string sqlite { get; set; }
    }
}
