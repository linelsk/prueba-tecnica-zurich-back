using biz.zurich.rarp.Entities;
using biz.zurich.rarp.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biz.zurich.rarp.Repository.Clientes
{
    public interface IClientesRepository : IGenericRepository<biz.zurich.rarp.Entities.Usuario>
    {
        List<Usuario> getClientes();
    }
}
