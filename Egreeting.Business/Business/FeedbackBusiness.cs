﻿using Egreeting.Business.IBusiness;
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
    public class FeedbackBusiness : GenericBusiness<Feedback>, IFeedbackBusiness
    {
        IFeedbackRepository FeedbackRepository;

        public FeedbackBusiness(EgreetingContext context = null)
          : base()
        {
            if (context == null)
            {
                context = new DesignTimeDbContextFactory().CreateDbContext(null);
            }
            this.context = context;

            this.FeedbackRepository = new FeedbackRepository(context);
            repository = FeedbackRepository;
        }
    }
}
