using GetJob.Models;
using GetJob.Models.UserModels;
using GetJob.Models.VacancyModel;

public class Employer : User, IAuth
{
    public List<Vacancy> Vacancies { get; set; }    
    public Employer() { }
    public Employer(int age, string name, string surname, string city, string phone, string mail, string username, string password)
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

    //vakansiya elave elesin
    //vakansiyani silsin
    //is axtarana teklif elesin


    //employer menu
}
