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
        string[] options = {"Employees", "Employers", "SignUp", "SignIn" };
        Menu LogInMenu = new Menu(options, 12, Console.LargestWindowHeight);
        int choice = LogInMenu.RunMenu();
        if (choice == 0) 
        { 
            //Eger emplyee-lere baxmaga daxil olubsa
            if(member == null)//guest kimi
            {

            }
            else//user kimi
            {

            }
        
        }
        else if (choice == 1) 
        { 
            if(member == null)//guest kimi
            {

            }
            else//user kimi
            {

            }
        
        }
        else if (choice == 2) //qeydiyyat
        {
            if(SignUpMenu(ref db))
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
            GuestMenu(ref db, ref member);
        }
    }

}
