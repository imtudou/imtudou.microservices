using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Domin.Repositories
{
    public interface IRepository <T> : IDisposable where T : class
    {
    }
}
