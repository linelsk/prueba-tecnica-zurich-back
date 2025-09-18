using biz.zurich.rarp.Entities;
using dal.flexform.rarp.Repository.Generic;
using dal.zurich.rarp.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.zurich.rarp.Repository.Clientes
{
    public class ClientesRepository : GenericRepository<Usuario>, biz.zurich.rarp.Repository.Clientes.IClientesRepository
    {
        public ClientesRepository(ZurichRarpContext context) : base(context)
        {
        }
        public List<Usuario> getClientes()
        {
            var clientesList = _context.Usuarios.ToList();

            return clientesList;
        }
    }
}
