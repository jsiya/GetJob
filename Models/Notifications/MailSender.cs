using System.Net.Mail;
using System.Net;

namespace GetJob.Models.Notifications;
public static class MailSender
{
    public static void MailVarification(string email)
    {
        string fromMail = "uselessmailaddress1221@gmail.com";
        string fromPassword = "xddgklkwtuffurlk";

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                Console.Clear();
                Random random = new Random();
                int check = random.Next(100000, 1000000);

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "Verification code";
                message.To.Add(new MailAddress(email));
                message.Body = check.ToString();
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };
                smtpClient.Send(message);

                Console.WriteLine("Enter the new 6-digit code:");
                string code = Console.ReadLine();
                if (code != check.ToString()) throw new Exception("Code is incorrect!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
                continue;
            }
            break;
        }
    }
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
