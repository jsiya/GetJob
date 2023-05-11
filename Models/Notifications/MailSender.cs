using System.Net.Mail;
using System.Net;

namespace GetJob.Models.Notifications;
public static class MailSender
{
    //random reqem yaradan funk
    public static void SendMail(Notification notification, string EmailAdress)
    {
        string fromMail = "uselessmailaddress1221@gmail.com";
        string fromPassword = "xddgklkwtuffurlk";

        MailMessage message = new MailMessage();
        message.From = new MailAddress(fromMail);
        message.Subject = "Notification!";
        message.To.Add(new MailAddress(EmailAdress));
        message.Body = notification.ToString();
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromMail, fromPassword),
            EnableSsl = true,
        };
        smtpClient.Send(message);
    }
}
