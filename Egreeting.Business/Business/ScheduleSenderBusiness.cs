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
    public class ScheduleSenderBusiness : GenericBusiness<ScheduleSender>, IScheduleSenderBusiness
    {
        IScheduleSenderRepository ScheduleSenderRepository;

        public ScheduleSenderBusiness(ILog logger, EgreetingContext context = null)
          : base(logger)
        {
            if (context == null)
            {
                context = new EgreetingContext();
            }
            this.context = context;

            this.ScheduleSenderRepository = new ScheduleSenderRepository(context);
            repository = ScheduleSenderRepository;
        }
    }
}
