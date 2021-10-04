using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A1Final.Models;

namespace A1Final.Controllers
{
    public class InteractiveTableController : Controller
    {
        // GET: InteractiveTable
        public ActionResult Index()
        {
            Entities en = new Entities();
            List<Booking> bookingnames = en.BookingSet.ToList();
            List<AspNetUsers> users = en.AspNetUsers.ToList();

            var interactivetable = from b in bookingnames
                                   join u in users on b.AspNetUsersId equals u.Id into table1
                                   from u in table1.DefaultIfEmpty()
                                   select new InteractiveTable { bookingdetails = b, userdetails = u };

            return View(interactivetable);
        }
    }
}