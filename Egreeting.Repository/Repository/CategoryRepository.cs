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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository()
        {
        }

        public CategoryRepository(EgreetingContext context)
            : base(context)
        {
        }
    }
}
