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
    public static IAuth SignInMenu()
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
            admin.SignIn(UsernameInput(),PasswordInput());
        }
        else if (choice == 1)
        {
            member = new Employee();
            Employee employee = member as Employee;
            employee.SignIn(UsernameInput(), PasswordInput());
        }
        else if (choice == 2)
        {
            member = new Employer();
            Employer employer = member as Employer;
            employer.SignIn(UsernameInput(), PasswordInput());
        }
        return member;
    }

    //Ne kimi sigUp olunmasi ucun secim menusu
    public static IAuth SignUpMenu()
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
            
        }
        else if (choice == 1)
        {

        }
        return null;
    }
    //Guest ucun secim menusu
    public static void GuestMenu()
    {        
        Database db = new Database();
        db.Reader();
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
            member = SignInMenu();
            if (member == null)
            {
                return;
            }
        }

        else if (choice == 3) 
        {
            member = SignUpMenu();
            if (member == null)
            {
                return;
            }
        }
    }

    //Esas menu olan dovr
    public static void MainMenu() 
    {
        while (true)
        {
            GuestMenu();
        }
    }

}
