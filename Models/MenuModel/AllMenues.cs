using GetJob.Models.AdminModel;
using GetJob.Models.DB;
using GetJob.Models.Notifications;
using MenuModel;

namespace GetJob.Models.MenuModel;

public static class AllMenues
{
    //Butun Employerleri gosteren menu
    public static void AllEmployersMenu(ref Database db, ref Member user)
    {
        List<string> users = new() {"<=back" };
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
            db.Employers.ElementAt(choice - 1).ViewCount++;
            var key = Console.ReadKey();
        }
    }

    //Butun Employeeleri gosteren menu
    public static void AllEmployeesMenu(ref Database db, ref Member user)
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
            db.Employees.ElementAt(choice - 1).ViewCount++;
            var key = Console.ReadKey();
        }
    }

    //notifler menusu
    public static void AllNotificatioMenu(Database db, Member user)
    {
        List<Notification> notifs = new();
        //gelen userin tipine gore db-den hemin uygun listedi notificationlari liste yigilir
        if (user is Admin)
        {
            notifs = db.Admins.FirstOrDefault(admin => admin.Id == user.Id).Notifications;
        }
        if (user is Employee)
        {
            notifs = db.Employees.FirstOrDefault(employee => employee.Id == user.Id).Notifications;
        }
        if (user is Employer)
        {
            notifs = db.Employers.FirstOrDefault(employer => employer.Id == user.Id).Notifications;
        }

        //yigilan notification-larin toStringlerini menyu ucun optionsa yigilir
        List<string> options = new() { "<=Back" };
        if (notifs != null)
        {
            options.AddRange(notifs.Select(notif => notif.ToString()));
        }
        Menu menu = new Menu(options.ToArray(), 12, 0);
        while (true)
        {
            Console.Clear();
            Logo.ShowNotificationsLogo();
            int choice = menu.RunMenu();
            if (choice == 0) break;
        }
    }

    //Ne kimi sigIn olunmasi ucun secim menusu
    public static Member SignInMenu(ref Database db)
    {
        Console.Clear();
        Member member = null;
        string[] options = { " Admin ", "Employee", "Employer", "< Back >" };
        Menu LogInMenu = new Menu(options, 12, Console.LargestWindowHeight);
        while (true)
        {
            Logo.ShowSignInLogo();
            int choice = LogInMenu.RunMenu();
            if (choice == 0)
            {
                member = new Admin();
                Admin admin = member as Admin;
                admin.SignIn(db);
                break;
            }
            else if (choice == 1)
            {
                member = new Employee();
                Employee employee = member as Employee;
                employee.SignIn(db);
                break;
            }
            else if (choice == 2)
            {
                member = new Employer();
                Employer employer = member as Employer;
                employer.SignIn(db);
                break;
            }
            else if (choice == 3) break;
        }
        //null qaytarsa guest olaraq qalacaq ele
        return member;
    }

    //Ne kimi sigUp olunmasi ucun secim menusu
    public static bool SignUpMenu(ref Database db)
    {
        Console.Clear();
        IAuth member = null;
        Logo.ShowSignUpLogo();
        string[] options = { "Employee", "Employer", "< Back >" };
        Menu LogInMenu = new Menu(options, 12, Console.LargestWindowHeight);
        int choice = LogInMenu.RunMenu();
        if (choice == 0)
        {
            member = new Employee();
            Employee employee = member as Employee;
            return employee.SignUp(ref db);
        }
        else if (choice == 1)
        {
            member = new Employer();
            Employer employer = member as Employer;
            return employer.SignUp(ref db);
        }
        return false;
    }

    //Guest ucun secim menusu
    public static void GuestMenu(ref Database db, ref Member member)
    {
        string[] options = { "Employees", "Employers", "Vacanies", " SignUp  ", " SignIn  " };
        Menu LogInMenu = new Menu(options, 12, Console.LargestWindowHeight);
        int choice = LogInMenu.RunMenu();
        if (choice == 0)//employees
        {
            AllEmployeesMenu(ref db, ref member);
        }
        else if (choice == 1)//employerler
        {
            AllEmployersMenu(ref db, ref member);
        }
        else if (choice == 2) //vakansiyalar
        {
            VacancyMenu.AllVacanciesMenu(ref db, ref member);
        }
        else if (choice == 3)//qeydiyyat
        {
            if (SignUpMenu(ref db))
            {
                choice = 3;//qeydiyyatdan kecdikden sonra logine atsin
            }
        }
        if (choice == 4) //login
        {
            member = SignInMenu(ref db);
            if (member == null)
            {
                return;
            }
        }
    }

    //userin menu
    public static void UserMenu(ref Database db, ref Member member)
    {
        string[] options = { "Employees", "Employers", "Vacancies", "Profile", "Notifications", "LogOut" };
        Menu LogInMenu = new Menu(options, 12, Console.LargestWindowHeight);
        int choice = LogInMenu.RunMenu();
        if (choice == 0) AllEmployeesMenu(ref db, ref member);
        else if (choice == 1) AllEmployersMenu(ref db, ref member);
        else if (choice == 2) VacancyMenu.AllVacanciesMenu(ref db, ref member);
        else if (choice == 3)//profile
        {
            if (member is Employee)
                EmployeeMenues.EmployeeProfileMenu(ref db, ref member);
            else
                EmployerMenues.EmployerProfileMenu(ref db, ref member);
        }
        else if (choice == 4)//notification
            AllNotificatioMenu(db, member);
        
        if (choice == 5) //logout
        {
            member = null;
        }
    }

    //Esas menu olan dovr
    public static void MainMenu()
    {
        Member member = null;
        //proqram baslayanda jsondan database-e oxunsun
        Database db = new Database();
        db.Reader();
        while (true)
        {
            Console.Clear();
            //GetJob Logosu
            Logo.ShowLogo();
            if (member == null || member.Username == null)//username-i null yoxlama sebebim signine daxil olub geri qayidanda orda secilenin default obyekti yaranir
                GuestMenu(ref db, ref member);
            else
            {
                if (member is Admin)
                    AdminMenues.AdminMenu(ref db, ref member);
                else
                    UserMenu(ref db, ref member);
            }
        }
    }

}
