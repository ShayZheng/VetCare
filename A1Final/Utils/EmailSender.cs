﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

namespace A1Final.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "YOUR API KEY HERE";

        public void Send(String toEmail, String subject, String contents, HttpPostedFileBase fileUploader)
        {
            //var client = new SendGridClient(API_KEY);
            //var from = new EmailAddress("noreply@localhost.com", "FIT5032 Example Email User");
            //var to = new EmailAddress(toEmail, "");
            //var plainTextContent = contents;
            //var htmlContent = "<p>" + contents + "</p>";
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = client.SendEmailAsync(msg);
            
            
            //write a suscribtionList(arraylist with user email) to contains the users who click the suscribtion options
            //foreach email address in 

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
            
            // Add attachment into mailMessage
            //var attachment = new System.Net.Mail.Attachment("C:\\Users\\yingzheng\\source\\repos\\A1Final\\Vets.jpg", MediaTypeNames.Image.Jpeg);

            //mailMessage.Attachments.Add(attachment);
            //mailMessage.To.Add(toEmail);
            
        }
    }
}