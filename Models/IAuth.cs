using MenuModel;

namespace GetJob.Models;
public interface IAuth
{
    void SignIn(string username, string password);
    void SignUp() { }
}
