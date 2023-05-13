using GetJob.Models.AdminModel;
using GetJob.Models.DB;
using MenuModel;

namespace GetJob.Models.MenuModel;

public static class AllMenues
{
    //console-dan username alir
    public static string UsernameInput()
    {
        Console.Clear();
        string username;
        Console.Write("Enter Username: ");
        username = Console.ReadLine();
        return username;
    }

    //consoledan passwordu alir (password gorsenmir)
    public static string PasswordInput()
    {
        //PrivateInput password-u **** kimi gostermek ucun
        PrivateInput privateInput = new PrivateInput();
        privateInput.InputPrivately();
        string password = privateInput.GetPrivateString();
        return password;
    }

    //bunun dovrunu duzelt!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //Ne kimi sigIn olunmasi ucun secim menusu
    public static IAuth SignInMenu(ref Database db)
    {
        Console.Clear();
        IAuth member = null;
        string[] options = { "Admin", "Employee", "Employer", "< Back >" };
        Menu LogInMenu = new Menu(options, 3, 25);
        int choice = LogInMenu.RunMenu();
        if (choice == 0)
        {
            member = new Admin();
            Admin admin = member as Admin;
            admin.SignIn(UsernameInput(),PasswordInput(), db);
        }
        else if (choice == 1)
        {
            member = new Employee();
            Employee employee = member as Employee;
            employee.SignIn(UsernameInput(), PasswordInput(), db);
        }
        else if (choice == 2)
        {
            member = new Employer();
            Employer employer = member as Employer;
            employer.SignIn(UsernameInput(), PasswordInput(), db);
        }
        return member;
    }

    //Ne kimi sigUp olunmasi ucun secim menusu
    public static IAuth SignUpMenu(ref Database db)
    {
        Console.Clear();
        IAuth member = null;
        string[] options = { "Employee", "Employer", "< Back >" };
        Menu LogInMenu = new Menu(options, 3, 25);
        int choice = LogInMenu.RunMenu();
        if (choice == 0)
        {
            member = new Employee();
            Employee employee = member as Employee;
            employee.SignUp(ref db);
        }
        else if (choice == 1)
        {
            member = new Employer();
            Employer employer = member as Employer;
            employer.SignUp(ref db);
        }
        return null;
    }
    //Guest ucun secim menusu
    public static void GuestMenu(ref Database db)
    {        
        Console.Clear();
        IAuth member = null;
        string[] options = {"Employees", "Employers", "SignIn", "SignUp" };
        Menu LogInMenu = new Menu(options, 3, 25);
        int choice = LogInMenu.RunMenu();
        if (choice == 0) 
        { 
        
        }
        else if (choice == 1) 
        { 
        
        }
        else if (choice == 2) 
        {
            member = SignInMenu(ref db);
            if (member == null)
            {
                return;
            }
        }

        else if (choice == 3) 
        {
            member = SignUpMenu(ref db);
            if (member == null)
            {
                return;
            }
        }
    }

    //Esas menu olan dovr
    public static void MainMenu() 
    {
        Database db = new Database();
        db.Reader();
        while (true)
        {
            GuestMenu(ref db);
        }
    }

}
