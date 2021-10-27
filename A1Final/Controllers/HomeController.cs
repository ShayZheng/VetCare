using A1Final.Models;
using System;
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
        Entities en = new Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Map()
        {
            ViewBag.Message = "This is a direction map.";

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

                    var client = new SmtpClient("smtp.mailtrap.io", 2525)
                    {
                        Credentials = new NetworkCredential("d671714f565af6", "c83da9acc600f6"),
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


        public ActionResult SendBulkEmail()
        {
            return View(new Models.BulkEmailViewModel());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult SendBulkEmail(Models.BulkEmailViewModel model, HttpPostedFileBase fileUploader)
        {
            
            if (ModelState.IsValid)
            {
                try
                {

                    

                    String subject = model.Subject;
                    String contents = model.Contents;

                    List<AspNetUsers> users = en.AspNetUsers.ToList();
                    //Get all the user email in database
                    List<string> emails = (from u in users select u.Email).ToList();
                    

                    var client = new SmtpClient("smtp.mailtrap.io", 2525)
                    {
                        Credentials = new NetworkCredential("d671714f565af6", "c83da9acc600f6"),
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
                    foreach (string userEmail in emails)
                    {
                        mailMessage.To.Add(userEmail);
                        client.Send(mailMessage);

                    };
                       
                    

                    //es.Send(toEmail, subject, contents, fileUploader);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new Models.BulkEmailViewModel());
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