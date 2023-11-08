using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Application.Authorization.Accounts.Dto
{
    public class RegisterOutput
    {
        public string Phone { get; set; }
        public DateTime RegisterTime{ get; set; }
    }
}
