using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Infrastructure
{
    public static class DateTimeExtension
    {
        public static DateTime ToBeginDate(this DateTime dt)
        {
            return DateTime.Parse(dt.ToString("yyyy-MM-dd 00:00:00"));
        }

        public static DateTime ToEndDate(this DateTime dt)
        {
            return DateTime.Parse(dt.ToString("yyyy-MM-dd 23:59:59"));
        }
    }
}
