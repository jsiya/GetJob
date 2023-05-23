using GetJob.Models.DB;
using GetJob.Models.Notifications;
using MenuModel;

namespace GetJob.Models.MenuModel;

public class AdminMenues
{
    //admin menu
    public static void AdminMenu(ref Database db, ref Member member)
    {
        string[] options = { "All Employers", "All Employees", "Deactive Vacancies", "Deactive Resumes", "Categories", "Notifications", "  < LogOut >  " };
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
        //butun employerlerin usernameini menuya yigir
        Menu menu = new Menu(users.ToArray(), 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.Clear();
            Logo.ShowEmployersLogo();
            choice = menu.RunMenu();
            if (choice == 0) break;//break secibse
            Console.Clear();
            Console.WriteLine(db.Employers.ElementAt(choice - 1).ToString()); // employerin infosu
            //Secim ya baxib ya da silecek
            Menu suggestMenu = new Menu(new string[] { "<=Back", "Delete" }, 12, 20);
            int choice2 = suggestMenu.RunMenu();
            //silmek
            if(choice2 == 1)
            {
                //maile bildiris getsin deye
                string email = db.Employers.ElementAt(choice - 1).Mail;
                //active ve ya deactive vakansiya varsa jsonda sile bilsi deye id saxlanir
                var id = db.Employers.ElementAt(choice - 1).Id;
                //silinen employere aid her sey silinir
                db.ActiveVacancies.RemoveAll(res => res.EmployerId == id);
                db.DeactiveVacancies.RemoveAll(res => res.EmployerId == id);
                db.Employers.Remove(db.Employers.ElementAt(choice - 1));
                MailSender.SendMail(new("Your account deleted!", DateTime.Now.ToString(), user), email);
                //jsona serialize
                db.Writer();
                break;
            } 
        }
    }

    //employerdeki eyni proses burda da olur
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
                var id = db.Employees.ElementAt(choice - 1).Id;
                db.ActiveResumes.RemoveAll(res => res.EmployeeId == id);
                db.DeactiveResumes.RemoveAll(res => res.EmployeeId == id);
                db.Employees.Remove(db.Employees.ElementAt(choice - 1));
                MailSender.SendMail(new("Your account deleted!", DateTime.Now.ToString(), member), email);
                db.Writer();
                break;
            }
        }
    }

    //deactive vakansiyalari activelesdirmek
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
                //eger geri qayitmagi secse isActive statuslarina ugun olaraq vakansiyalar uygun jsonlara yazilir
                VacancyMenu.TransferVacancies(ref db);
                break;
            }
            //eks halda her secimde secilen vakansiyanin isActive ve Showable-in statusu deyisir
            //yeni activedise deaktiv, deactivrdise active olur, break edende son versiya saxlanilir
            db.DeactiveVacancies.ElementAt(choice-1).IsActive = !db.DeactiveVacancies.ElementAt(choice-1).IsActive;
            if(db.DeactiveVacancies.ElementAt(choice - 1).IsActive == true)//eger activelesdirlse expire date-e uygu olaraq showable edilir
                if(db.DeactiveVacancies.ElementAt(choice-1).ExpireDate >  DateTime.Now)
                    db.DeactiveVacancies.ElementAt(choice - 1).Showable = !db.DeactiveVacancies.ElementAt(choice - 1).Showable;
        }
    }
    //resume ucun de eyni vakansiyadaki proses tekrarlanir
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
