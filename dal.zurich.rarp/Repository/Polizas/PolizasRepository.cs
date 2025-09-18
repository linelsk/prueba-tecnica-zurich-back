using biz.zurich.rarp.Entities;
using biz.zurich.rarp.Repository.Polizas;
using biz.zurich.rarp.Repository.Generic;
using dal.zurich.rarp.DBContext;
using dal.flexform.rarp.Repository.Generic;
using System.Linq.Expressions;

namespace dal.zurich.rarp.Repository.Polizas
{
    public class PolizasRepository : GenericRepository<Poliza>, IPolizasRepository
    {
        public PolizasRepository(ZurichRarpContext context) : base(context)
        {
        }
        // Métodos específicos para polizas si es necesario
    }
}