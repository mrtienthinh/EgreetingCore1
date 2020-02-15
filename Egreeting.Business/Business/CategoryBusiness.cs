using Egreeting.Business.IBusiness;
using Egreeting.Models.AppContext;
using Egreeting.Models.Models;
using Egreeting.Repository.IRepository;
using Egreeting.Repository.Repository;
using log4net;

namespace Egreeting.Business.Business
{
    public class CategoryBusiness : GenericBusiness<Category>, ICategoryBusiness
    {
        ICategoryRepository CategoryRepository;

        public CategoryBusiness(EgreetingContext context = null)
          : base()
        {

            if (context == null)
            {
                context = new DesignTimeDbContextFactory().CreateDbContext(null);
            }
            this.context = context;

            this.CategoryRepository = new CategoryRepository(context);
            repository = CategoryRepository;
        }
    }
}
