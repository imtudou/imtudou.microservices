using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Models
{
    public class UserInfoModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name 必填！")]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public GB_GenderEnum Gender { get; set; }
        
    }

    public enum GB_GenderEnum
    {
        [Description("未知的性别")]
        Unknown = 0,

        [Description("男性")]
        Man = 1,

        [Description("女性")]
        Women = 2,

        [Description("未说明的性别")]
        Unspecified = 9
    }
}
