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
    public class EcardRepository : GenericRepository<Ecard>, IEcardRepository
    {
        public EcardRepository()
        {
        }

        public EcardRepository(EgreetingContext context)
            : base(context)
        {
        }
    }
}
