using GetJob.Models;
using GetJob.Models.DB;
using GetJob.Models.MenuModel;
using GetJob.Models.UserModels;
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
        //City = city;
        Phone = phone;
        Mail = mail;
        Username = username;
        Password = password;
    }

    public void SignIn(string username, string password, Database db)
    {
        var user = db.Employees.Find(employee => employee.Username == username && employee.Password == password);
        if(user != null)
        {

        }
    }

    public void SignUp(ref Database db)
    {
        string name, surname, email, username,phone, age;
        PrivateInput password = new();
        //int age = 0;
        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter Name: ");
            name = Console.ReadLine();
        }while(!ExceptionHandling.ForName(name));

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter Surname: ");
            surname = Console.ReadLine();
        } while (!ExceptionHandling.ForSurname(surname));

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter Age: ");
            age = Console.ReadLine();

        } while (!ExceptionHandling.ForAge(age));

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter Email Adress: ");
            email = Console.ReadLine();
        } while (!ExceptionHandling.ForMail(email));

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter Phone: ");
            phone = Console.ReadLine();
        } while (!ExceptionHandling.ForPhone(phone));

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter Username: ");
            username = Console.ReadLine();
        } while (!ExceptionHandling.ForUsername(username, db));

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            password.InputPrivately();
        } while (!ExceptionHandling.ForPassword(password.GetPrivateString()));
        //Yeni employee yaradib liste elave edilib file- a yazildi
        db.Employees.Add(new Employee(Int32.Parse(age), name, surname, phone, email, username, password.GetPrivateString()));
        db.Writer();//her defe yazsin yoxsa sonda bir defe yazar?
    }

    //ise apply etsin
    //cv elave elesin //add methodu generic
    //cv-ni silsin

    //employee menu
}
