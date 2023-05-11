using GetJob.Models;
using GetJob.Models.UserModels;
public class Employee : User, IAuth
{
    public List<Resume> Resumes { get; set; }
    public Employee() { }
    public Employee(int age, string name, string surname, string city, string phone, string mail, string username, string password)
    {
        Id = new Guid();
        Age = age;
        Name = name;
        Surname = surname;
        City = city;
        Phone = phone;
        Mail = mail;
        Username = username;
        Password = password;
    }

    public void SignIn(string username, string password)
    {
        throw new NotImplementedException();
    }

    public void SignUp()
    {

    }

    //ise apply etsin
    //cv elave elesin //add methodu generic
    //cv-ni silsin

    //employee menu
}
