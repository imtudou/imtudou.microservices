using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Application.Authorization.Accounts.Dto
{
    public class RegisterInput
    {
        public string Phone{ get; set; }
        public string IDCard { get; set; }
        public string Pwd { get; set; }
        public string ConfirmPwd { get; set; }
    }
}
