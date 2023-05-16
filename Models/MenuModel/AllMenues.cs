using GetJob.Models.AdminModel;
using GetJob.Models.DB;
using MenuModel;

namespace GetJob.Models.MenuModel;

public static class AllMenues
{
    //Butun userleri gosteren menu
    public static void AllEmployersMenu(ref Database db, ref IAuth user)
    {
        List<string> users = db.Employers.Select(x => x.Username).ToList();
        users.Add("<=Back");

        Menu menu = new Menu(users.ToArray(), 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.Clear();
            Logo.ShowEmployersLogo();
            choice = menu.RunMenu();
            if (choice == users.Count - 1) break;
            
        }
    }

    //Ne kimi sigIn olunmasi ucun secim menusu
    public static IAuth SignInMenu(ref Database db)
    {
        Console.Clear();
        IAuth member = null;
        string[] options = { "Admin", "Employee", "Employer", "< Back >" };
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
    public static void GuestMenu(ref Database db, ref IAuth member)
    {
        string[] options = { "Employees", "Employers", "SignUp", "SignIn" };
        Menu LogInMenu = new Menu(options, 12, Console.LargestWindowHeight);
        int choice = LogInMenu.RunMenu();
        if (choice == 0)
        {

        }
        else if (choice == 1)
        {
            AllEmployersMenu(ref db, ref member);
        }
        else if (choice == 2) //qeydiyyat
        {
            if (SignUpMenu(ref db))
            {
                choice = 3;//qeydiyyatdan kecdikden sonra logine atsin
            }
        }
        if (choice == 3) //login
        {
            member = SignInMenu(ref db);
            if (member == null)
            {
                return;
            }
        }
    }

    public static void HomePageMenu(ref Database db, ref IAuth member)
    {
        string[] options = { "Employees", "Employers", "Profile", "LogOut" };
        Menu LogInMenu = new Menu(options, 12, Console.LargestWindowHeight);
        int choice = LogInMenu.RunMenu();
        if (choice == 0)
        {

        }
        else if (choice == 1)
        {
            AllEmployersMenu(ref db, ref member);
        }
        else if (choice == 2) //profile
        {
            
        }
        if (choice == 3) //logout
        {
            member = null;
        }
    }

    //Esas menu olan dovr
    public static void MainMenu()
    {
        IAuth member = null;
        //proqram baslayanda jsondan database-e oxunsun
        Database db = new Database();
        db.Reader();
        while (true)
        {
            Console.Clear();
            //GetJob Logosu
            Logo.ShowLogo();
            if(member == null)
                GuestMenu(ref db, ref member);
            else
                HomePageMenu(ref db, ref member);
        }
    }

}
