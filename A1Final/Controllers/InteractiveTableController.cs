﻿using System;
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
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            Entities en = new Entities();
            List<Booking> bookingnames = en.BookingSet.ToList();
            List<AspNetUsers> users = en.AspNetUsers.ToList();
            List<Vets> vets = en.VetsSet.ToList();

            //validate the table data, if it is null then return the error page and redirect to the home page.
            var interactivetable = from b in bookingnames
                                   join u in users on b.AspNetUsersId equals u.Id into table1
                                   from u in table1.DefaultIfEmpty()
                                   join v in vets on b.VetsId equals v.Id into table2
                                   from v in table2.DefaultIfEmpty()
                                   select new InteractiveTable { bookingdetails = b, userdetails = u, vetdetails = v };

            return View(interactivetable);
        }

       
    }
}