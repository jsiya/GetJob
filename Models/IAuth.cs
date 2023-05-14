using GetJob.Models.DB;
using MenuModel;

namespace GetJob.Models;
public interface IAuth
{
    void SignIn(Database db);
    void SignUp(ref Database db) { }
}
