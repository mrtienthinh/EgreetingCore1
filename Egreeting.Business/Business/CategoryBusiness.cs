using Egreeting.Business.IBusiness;
using Egreeting.Models.AppContext;
using Egreeting.Models.Models;
using Egreeting.Repository.IRepository;
using Egreeting.Repository.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Business.Business
{
    public class CategoryBusiness : GenericBusiness<Category>, ICategoryBusiness
    {
        ICategoryRepository CategoryRepository;

        public CategoryBusiness(ILog logger, EgreetingContext context = null)
          : base(logger)
        {

            if (context == null)
            {
                context = new EgreetingContext();
            }
            this.context = context;

            this.CategoryRepository = new CategoryRepository(context);
            repository = CategoryRepository;
        }
    }
}
