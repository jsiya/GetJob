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
        Id = new Guid();
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

    public void SignIn(string username, string password, Database db)
    {
        var user = db.Employees.Find(employee => employee.Username == username && employee.Password == password);
        if (user != null)
        {

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
                Console.SetCursorPosition(70, 12);
                name = Console.ReadLine();
                if (!ExceptionHandling.ForName(ref name));
            }
            else if (choice == 1)
            {
                do
                {
                    Console.SetCursorPosition(70, 13);
                    surname = Console.ReadLine();
                } while (!ExceptionHandling.ForSurname(ref surname));
            }
            else if (choice == 2)
            {
                do
                {
                    Console.SetCursorPosition(70, 14);
                    age = Console.ReadLine();
                } while (!ExceptionHandling.ForAge(ref age));
            }
            else if (choice == 3)
            {
                do
                {
                    Console.SetCursorPosition(70, 15);
                    email = Console.ReadLine();
                } while (!ExceptionHandling.ForMail(ref email));
            }

            else if (choice == 4)
            {
                do
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(70, 16);
                    phone = Console.ReadLine();
                } while (!ExceptionHandling.ForPhone(ref phone));
            }
            else if (choice == 5)
            {
                do
                {
                    Console.SetCursorPosition(70, 17);
                    username = Console.ReadLine();
                } while (!ExceptionHandling.ForUsername(ref username, db));
            }
            else if (choice == 6)
            {
                do
                {
                    Console.SetCursorPosition(70, 18);
                    password.InputPrivately();
                } while (!ExceptionHandling.ForPassword(password.GetPrivateString()));
            }
            else if (choice == 7 && name != "" && surname != "" && email != "" && username != "" && password.ToString() != "" && age != "" && phone != "")
            {
                MailSender.MailVarification(email);
                db.Employees.Add(new Employee(Int32.Parse(age), name, surname, phone, email, username, password.GetPrivateString()));
                db.Writer();
                return true;
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
