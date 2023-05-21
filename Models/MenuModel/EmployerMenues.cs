using GetJob.Models.DB;
using GetJob.Models.Notifications;
using MenuModel;

namespace GetJob.Models.MenuModel;

public static class EmployerMenues
{
    public static void EmployerProfileMenu(ref Database db, ref Member user)
    {
        Console.Clear();
        string[] options = { "Create Vacancy", "Show My Vacancies", "Update Personal Info", "< Back >" };
        Menu menu = new Menu(options, 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.Clear();
            Logo.ShowProfileLogo();
            choice = menu.RunMenu();
            if (choice == 0) VacancyMenu.CreateVacancyMenu(ref db, ref user);
            else if (choice == 1) VacancyMenu.ShowUsersVacanciesMenu(ref db, ref user);
            else if (choice == 2) UpdatePersonalInfoMenu(ref db, ref user);
            else break;
        }
    }
    public static void UpdatePersonalInfoMenu(ref Database db, ref Member user)
    {
        List<string> options = new();
        Employer employer = null;
        employer = user as Employer;
        string username = employer.Username;
        string password = employer.Password;
        string city = employer.City;
        string email = employer.Mail;
        string phone = employer.Phone;

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
                    employer.Username = username;
                    employer.Password = password;
                    employer.City = city;
                    employer.Mail = email;
                    employer.Phone = phone;
                    db.Writer();
                    employer.Notifications.Add(new Notification("Personal Info Updated",DateTime.Now.ToString(), employer));
                    MailSender.SendMail(employer.Notifications.Last(), employer.Mail);
                }
            }
            else break;
        }
    }

    public static void ApplyEmployeeMenu(ref Database db, ref Member user, int index)//vakansiyaya gelen muracietler
    {
        int count = db.ActiveVacancies.ElementAt(index).Appliers.Count;
        List<Employee> employees = new List<Employee>();
        foreach (var item in db.ActiveVacancies.ElementAt(index).Appliers)
        {
            employees.Add(db.Employees.FirstOrDefault(employer => employer.Id == item));
        }
        List<string> ops = new() { "<=Back" };
        ops.AddRange(employees.Select(emp => emp.Username).ToList());

        Menu menu = new Menu(ops.ToArray(), 4, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.Clear();
            Console.WriteLine(@"                                                Appliers of this vacancy: ");
            choice = menu.RunMenu();
            if (choice == 0) break;
            try
            {
                Console.WriteLine(employees.ElementAt(choice - 1).Resumes.LastOrDefault(cv => cv.Showable == true).ToString());
                employees.ElementAt(choice - 1).Resumes.LastOrDefault(cv => cv.Showable == true).ViewCount++;
            }
            catch
            {
                Console.WriteLine($"{employees.ElementAt(choice - 1).Name} does not have any Resume!");
            }
            Console.WriteLine("Do you want to accept this appeal?");
            List<string> ops2 = new() { "Yes", "No" };
            Menu menu2 = new Menu(ops2.ToArray(), 10, Console.LargestWindowHeight);
            int choice2 = menu2.RunMenu();
            if (choice2 == 0) MailSender.SendMail(new Notification($"Congratulation {employees.ElementAt(choice - 1).Name}! Your appeal for {db.ActiveVacancies.ElementAt(index).Title} was accepted!", DateTime.Now.ToString(), user), employees.ElementAt(choice - 1).Mail);
            else MailSender.SendMail(new Notification($"Hope you are well {employees.ElementAt(choice - 1).Name}! Unfortunately, your appeal for {db.ActiveVacancies.ElementAt(index).Title} was rejected!", DateTime.Now.ToString(), user), employees.ElementAt(choice - 1).Mail);
        }

    }
}
