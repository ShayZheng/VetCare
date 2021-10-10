﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace A1Final.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Send_Email()
        {
            return View(new Models.SendEmailViewModel());
        }

        [HttpPost]
        public ActionResult Send_Email(Models.SendEmailViewModel model, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    //Utils.EmailSender es = new Utils.EmailSender();


                    var client = new SmtpClient("smtp.mailtrap.io", 2525)
                    {
                        Credentials = new NetworkCredential("2ad108d5eaa64b", "a77e262be93e25"),
                        EnableSsl = true
                    };

                    var plainTextContent = contents;

                    //Add a mailmessage which includes from,subject and body.
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("from@example.com"),
                        Subject = subject,
                        Body = contents,
                        IsBodyHtml = true,
                    };
                    // Add attachment into mailMessage
                    if (fileUploader != null)
                    {
                        string fileName = Path.GetFileName(fileUploader.FileName);
                        var attachment = new Attachment(fileUploader.InputStream, fileName);
                        mailMessage.Attachments.Add(attachment);
                    }

                    mailMessage.To.Add(toEmail);

                    client.Send(mailMessage);
                    //es.Send(toEmail, subject, contents, fileUploader);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new Models.SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}