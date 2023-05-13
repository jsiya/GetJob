using GetJob.Models.DB;

namespace GetJob.Models.AdminModel;
public class Admin: Member, IAuth
{
    //kateqoriyalar burda olsun?
    //vakansiyalar olsun tesdiqlenmemisler

    public Admin() { }
    public Admin(string email, string username, string password)
    {
        Id = new Guid();
        Mail = email;
        Username = username;
        Password = password;
    }

    public void SignIn(string username, string password, Database db)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"id={Id}\nmail={Mail}\nusername={Username}\npassword={Password}";
    }
    //user yaratsin
    //user silsin
    //user usere baxsin
    //useri edit etsin
    //employeelerin cv-lerinin tesdiqlesin
    //employerlerin vakansiyalarini tesdiqlesin

}

