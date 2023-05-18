using GetJob.Models;
using GetJob.Models.DB;
using GetJob.Models.MenuModel;
using GetJob.Models.Notifications;
using GetJob.Models.UserModels;
using MenuModel;

public class Employee : User, IAuth
{
    public List<Resume> Resumes { get; set; }
    public Employee() { }
    public Employee(int age, string name, string surname, string phone, string mail, string username, string password)
    {
        Id = Guid.NewGuid();
        Age = age;
        Name = name;
        Surname = surname;
        City = "";
        Phone = phone;
        Mail = mail;
        Username = username;
        Password = password;
        Resumes = new List<Resume>();
        Notifications = new List<Notification>();
    }
    
    public void SignIn(Database db)
    {
        string username = "";
        PrivateInput password = new();
        string[] options = { "Username: ", "Password: ", "<Commit>", "<=Back" };
        Menu menu = new Menu(options, 12, Console.LargestWindowHeight);
        while (true)
        {
            Console.ResetColor();
            Console.Clear();
            menu._menuList[0] = $"Username: {username}";
            menu._menuList[1] = $"Password: {password}";
            Logo.ShowSignInLogo();
            int choice = menu.RunMenu();
            if (choice == 0)
            {
                Console.SetCursorPosition(72, 12);
                username = Console.ReadLine();
            }
            else if (choice == 1)
            {
                Console.SetCursorPosition(72, 13);
                password.InputPrivately();
            }
            else if (choice == 2)
            {
                //eger bu user varsa bu obyekte fieldleri assign edir
                var user = db.Employees.Find(employee => employee.Username == username && employee.Password == password.GetPrivateString());
                if (user != null)
                {
                    this.Id = user.Id;
                    this.Surname = user.Surname;
                    this.Name = user.Name;
                    this.Age = user.Age;
                    this.City = user.City;
                    this.Mail = user.Mail;
                    this.Phone = user.Phone;
                    this.Username = user.Username;
                    this.Password = password.GetPrivateString();
                    this.Resumes = user.Resumes;
                    this.Notifications = user.Notifications;
                    break;
                }
                else
                {
                    Console.SetCursorPosition(57, 11);
                    Console.WriteLine("Username doesn't exist!");
                    Thread.Sleep(1000);
                }
            }
            if (choice == 3)
            {
                break;
            }
        }
    }

    public bool SignUp(ref Database db)
    {
        string name = "", surname = "", email = "", username = "", phone = "", age = "";
        PrivateInput password = new();
        string[] options = { "Name: ", "Surname: ", "Age: ", "Email: ", "Phone: ", "Username: ", "Password: ", "<Commit>", "<=Back" };
        Menu menu = new Menu(options, 12, Console.LargestWindowHeight);
        while (true)
        {
            Console.ResetColor();
            Console.Clear();
            menu._menuList[0] = $"Name:     {name}";
            menu._menuList[1] = $"Surname:  {surname}";
            menu._menuList[2] = $"Age:      {age}";
            menu._menuList[3] = $"Email:    {email}";
            menu._menuList[4] = $"Phone:    {phone}";
            menu._menuList[5] = $"Username: {username}";
            menu._menuList[6] = $"Password: {password}";
            Logo.ShowSignUpLogo();
            int choice = menu.RunMenu();
            if (choice == 0)
            {
                Console.SetCursorPosition(72, 12);
                name = Console.ReadLine();
                if (!ExceptionHandling.ForName(ref name))
                    continue;
            }
            if (choice == 1)
            {
                Console.SetCursorPosition(72, 13);
                surname = Console.ReadLine();
                if (!ExceptionHandling.ForSurname(ref surname))
                    continue;
            }
            if (choice == 2)
            {
                Console.SetCursorPosition(72, 14);
                age = Console.ReadLine();
                if (!ExceptionHandling.ForAge(ref age))
                    continue;
            }
            if (choice == 3)
            {
                Console.SetCursorPosition(72, 15);
                email = Console.ReadLine();
                if (!ExceptionHandling.ForMail(ref email))
                    continue;
            }

            if (choice == 4)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(72, 16);
                phone = Console.ReadLine();
                if (!ExceptionHandling.ForPhone(ref phone))
                    continue;
            }
            if (choice == 5)
            {
                Console.SetCursorPosition(72, 17);
                username = Console.ReadLine();
                if (!ExceptionHandling.ForUsername(ref username, db))
                    continue;
            }
            if (choice == 6)
            {
                Console.SetCursorPosition(72, 18);
                password.InputPrivately();
                if (!ExceptionHandling.ForPassword(password.GetPrivateString()))
                {
                    password.privateString = "";
                    continue;
                }
            }
            else if (choice == 7 && name != "" && surname != "" && email != "" && username != "" && password.ToString() != "" && age != "" && phone != "")
            {
                if (MailSender.MailVarification(email))
                {
                    db.Employees.Add(new Employee(Int32.Parse(age), name, surname, phone, email, username, password.GetPrivateString()));
                    db.Writer();
                    return true;
                }
            }
            else if (choice == 8)
            {
                break;
            }
        }
        return false;
    }

    //ise apply etsin
    //cv elave elesin //add methodu generic
    //cv-ni silsin

    //employee menu
}
