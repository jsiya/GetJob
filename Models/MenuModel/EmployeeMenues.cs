using GetJob.Models.DB;
using GetJob.Models.Notifications;
using MenuModel;
using System.Xml.Linq;

namespace GetJob.Models.MenuModel;

public static class EmployeeMenues
{
    //profile
    public static void EmployeeProfileMenu(ref Database db, ref Member user)
    {
        Console.Clear();
        string[] options = { "Create Resume", "Show My Resumes", "Update Personal Info", "< Back >" };
        Menu menu = new Menu(options, 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.Clear();
            Logo.ShowProfileLogo();
            choice = menu.RunMenu();
            if (choice == 0) ResumeMenu.CreateResumeMenu(ref db, ref user);
            else if (choice == 1) ResumeMenu.ShowUsersResumesMenu(ref db, ref user);
            else if (choice == 2) UpdatePersonalInfoMenu(ref db, ref user);
            else break;
        }
    }

    public static void UpdatePersonalInfoMenu(ref Database db, ref Member user)
    {
        List<string> options = new();
        Employee employee = null;
        employee = user as Employee;
        string username = employee.Username;
        string password = employee.Password;
        string city = employee.City;
        string email = employee.Mail;
        string phone = employee.Phone;

        options.Add($"Username: {username}");
        options.Add($"Password: {password}");
        options.Add($"City: {city}");
        options.Add($"Mail: {email}");
        options.Add($"Phone: {phone}");
        options.Add("<<Commit>>");
        options.Add("<<Back>>");
        Menu menu = new Menu(options.ToArray(), 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.Clear();
            Console.ResetColor();
            Logo.ShowProfileLogo();
            menu._menuList[0] = $"Username: {username}";
            menu._menuList[1] = $"Password: {password}";
            menu._menuList[2] = $"City: {city}";
            menu._menuList[3] = $"Email:    {email}";
            menu._menuList[4] = $"Phone:    {phone}";
            choice = menu.RunMenu();
            if (choice == 0)
            {
                Console.SetCursorPosition(72, 12);
                username = Console.ReadLine();
                if (!ExceptionHandling.ForUsername(ref username, db))
                    continue;
            }
            else if (choice == 1)
            {
                Console.SetCursorPosition(72, 13);
                password = Console.ReadLine();
                if (!ExceptionHandling.ForPassword(password))
                    continue;
            }
            else if (choice == 2)
            {
                Console.SetCursorPosition(72, 14);
                city = Console.ReadLine();
                if (!ExceptionHandling.ForCity(ref city))
                    continue;
            }
            else if (choice == 3)
            {
                Console.SetCursorPosition(72, 15);
                email = Console.ReadLine();
                if (!ExceptionHandling.ForMail(ref email))
                    continue;
            }
            else if (choice == 4)
            {
                Console.SetCursorPosition(72, 16);
                phone = Console.ReadLine();
                if (!ExceptionHandling.ForPhone(ref phone))
                    continue;
            }
            else if (choice == 5)
            {
                if (MailSender.MailVarification(email))
                {
                    employee.Username = username;
                    employee.Password = password;
                    employee.City = city;
                    employee.Mail = email;
                    employee.Phone = phone;
                    db.Writer();
                }
            }
            else break;
        }
    }
}
