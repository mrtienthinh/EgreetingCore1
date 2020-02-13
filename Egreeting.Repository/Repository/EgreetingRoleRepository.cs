using Egreeting.Models.AppContext;
using Egreeting.Models.Models;
using Egreeting.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Repository.Repository
{
    public class EgreetingRoleRepository : GenericRepository<EgreetingRole>, IEgreetingRoleRepository
    {
        public EgreetingRoleRepository()
        {
        }

        public EgreetingRoleRepository(EgreetingContext context)
            : base(context)
        {
        }
    }
}
