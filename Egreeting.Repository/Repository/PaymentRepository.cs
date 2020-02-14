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
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository()
        {
        }

        public PaymentRepository(EgreetingContext context)
            : base(context)
        {
        }
    }
}
