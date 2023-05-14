using GetJob.Models;
using GetJob.Models.DB;
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
    public bool SignUp(ref Database db)
    {
        return false;
    }

    public void SignIn(string username, string password, Database db)
    {
        throw new NotImplementedException();
    }

    //vakansiya elave elesin
    //vakansiyani silsin
    //is axtarana teklif elesin


    //employer menu
}
