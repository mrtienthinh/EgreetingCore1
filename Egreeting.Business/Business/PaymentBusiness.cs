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
    public class PaymentBusiness : GenericBusiness<Payment>, IPaymentBusiness
    {
        IPaymentRepository PaymentRepository;

        public PaymentBusiness(EgreetingContext context = null)
          : base()
        {
            if (context == null)
            {
                context = new DesignTimeDbContextFactory().CreateDbContext(null);
            }
            this.context = context;

            this.PaymentRepository = new PaymentRepository(context);
            repository = PaymentRepository;
        }
    }
}
