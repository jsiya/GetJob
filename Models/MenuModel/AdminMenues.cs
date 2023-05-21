using GetJob.Models.DB;
using GetJob.Models.Notifications;
using GetJob.Models.UserModels;
using MenuModel;

namespace GetJob.Models.MenuModel;

public class AdminMenues
{
    //admin menu
    public static void AdminMenu(ref Database db, ref Member member)
    {
        string[] options = { "All Employers", "All Employees", "Vacancies", "Resumes", "Categories", "Notifications", "  < LogOut >  " };
        Menu menu = new Menu(options, 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.ResetColor();
            Console.Clear();
            Logo.ShowAdminLogo();
            choice = menu.RunMenu();
            if (choice == 0)
                AllEmployers(ref db, ref member);
            else if (choice == 1)
                AllEmployees(ref db, ref member);
            else if (choice == 2)
                ActivateAndDeactivateMenu(ref db, ref member);
            else if (choice == 3)
                ActivateAndDeactivateResumeMenu(ref db, ref member);
            else if (choice == 4)
                CategoryMenues.Categories(ref db, ref member);
            else if (choice == 5)
                AllMenues.AllNotificatioMenu(db, member);
            else if (choice == 6)
            {
                member = null;
                break;
            }
        }
    }


    public static void AllEmployers(ref Database db, ref Member user)
    {
        List<string> users = new() { "<=back" };
        users.AddRange(db.Employers.Select(x => x.Username).ToList());

        Menu menu = new Menu(users.ToArray(), 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.Clear();
            Logo.ShowEmployersLogo();
            choice = menu.RunMenu();
            if (choice == 0) break;
            Console.Clear();
            Console.WriteLine(db.Employers.ElementAt(choice - 1).ToString());
            Menu suggestMenu = new Menu(new string[] { "<=Back", "Delete" }, 12, 20);
            int choice2 = suggestMenu.RunMenu();
            if(choice2 == 1)
            {
                string email = db.Employers.ElementAt(choice - 1).Mail;
                db.Employers.Remove(db.Employers.ElementAt(choice - 1));
                MailSender.SendMail(new("Your account deleted!", DateTime.Now.ToString(), user), email);
                db.Writer();
                break;
            } 
        }
    }

    public static void AllEmployees(ref Database db, ref Member member)
    {
        List<string> users = new() { "<=back" };
        users.AddRange(db.Employees.Select(x => x.Username).ToList());

        Menu menu = new Menu(users.ToArray(), 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.Clear();
            Logo.ShowEmployeesLogo();
            choice = menu.RunMenu();
            if (choice == 0) break;
            Console.Clear();
            Console.WriteLine(db.Employees.ElementAt(choice - 1).ToString());
            Menu suggestMenu = new Menu(new string[] { "<=Back", "Delete" }, 12, 20);
            int choice2 = suggestMenu.RunMenu();
            if (choice2 == 1)
            {
                string email = db.Employees.ElementAt(choice - 1).Mail;
                db.Employees.Remove(db.Employees.ElementAt(choice - 1));
                MailSender.SendMail(new("Your account deleted!", DateTime.Now.ToString(), member), email);
                db.Writer();
                break;
            }
        }
    }

    public static void ActivateAndDeactivateMenu(ref Database db, ref Member member)
    {
        int choice;
        while (true)
        {
            Console.ResetColor();
            Console.Clear();
            Logo.ShowAdminLogo();
            List<string> options = new() { "<=Back" };
            options.AddRange(db.DeactiveVacancies.Select(item => item.ToStringForAdmin()).ToList());
            Menu menu = new Menu(options.ToArray(), 12, Console.LargestWindowHeight);
            choice = menu.RunMenu();
            if (choice == 0)
            {
                VacancyMenu.TransferVacancies(ref db);
                break;
            }
            db.DeactiveVacancies.ElementAt(choice-1).IsActive = !db.DeactiveVacancies.ElementAt(choice-1).IsActive;
            if(db.DeactiveVacancies.ElementAt(choice - 1).IsActive == true)
                if(db.DeactiveVacancies.ElementAt(choice-1).ExpireDate >  DateTime.Now)
                    db.DeactiveVacancies.ElementAt(choice - 1).Showable = !db.DeactiveVacancies.ElementAt(choice - 1).Showable;
        }
    }
    public static void ActivateAndDeactivateResumeMenu(ref Database db, ref Member member)
    {
        int choice;
        while (true)
        {
            Console.ResetColor();
            Console.Clear();
            Logo.ShowAdminLogo();
            List<string> options = new() { "<=Back" };
            options.AddRange(db.DeactiveResumes.Select(item => item.ToString()).ToList());
            Menu menu = new Menu(options.ToArray(), 12, Console.LargestWindowHeight);
            choice = menu.RunMenu();
            if (choice == 0)
            {
                ResumeMenu.TransferResumes(ref db);
                break;
            }
            db.DeactiveResumes.ElementAt(choice - 1).IsActive = !db.DeactiveResumes.ElementAt(choice - 1).IsActive;
            if (db.DeactiveResumes.ElementAt(choice - 1).IsActive == true)
                    db.DeactiveResumes.ElementAt(choice - 1).Showable = !db.DeactiveResumes.ElementAt(choice - 1).Showable;
        }
    }
}
