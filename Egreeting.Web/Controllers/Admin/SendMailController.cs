using Egreeting.Models.AppContext;
using Egreeting.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Egreeting.Web.Controllers.Admin
{
    public class SendMailController : BaseController
    {

        // GET: SendMail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendByOrder(int? ItemID)
        {
            Utils.Utils.SendMailByOrder(ItemID);
            return Redirect(Request.Headers["UrlReferrer"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Sendall()
        {
            using (var context = new EgreetingContext())
            {
                var ListItemID = context.Set<Order>().Where(x => x.ScheduleTime > DateTime.Now && x.Draft != null).Select(x => x.OrderID).ToList();
                Utils.Utils.SendMailAll(ListItemID);
            }
            return Redirect(Request.Headers["UrlReferrer"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendByOrderDetail(int? ItemID)
        {
            Utils.Utils.SendMailByOrderDetail(ItemID);
            return Redirect(Request.Headers["UrlReferrer"].ToString());
        }
    }
}