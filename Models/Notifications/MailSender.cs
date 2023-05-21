using System.Net.Mail;
using System.Net;
using GetJob.Models.MenuModel;

namespace GetJob.Models.Notifications;
public static class MailSender
{
    public static bool MailVarification(string email)
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

                Logo.ShowVarificationLogo();
                Console.SetCursorPosition(40, 15);
                Console.Write("Enter the new 6-digit code:");

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
                Console.SetCursorPosition(70, 15);
                string code = Console.ReadLine();
                if (code != check.ToString()) throw new Exception("Code is incorrect!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
                //continue;
                return false;
            }
            return true;
        }
    }
    public static void SendMail(Notification notification, string EmailAdress)
    {
        string fromMail = "uselessmailaddress1221@gmail.com";
        string fromPassword = "xddgklkwtuffurlk";

        MailMessage message = new MailMessage();
        message.From = new MailAddress(fromMail);
        message.Subject = "GetJob";
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
