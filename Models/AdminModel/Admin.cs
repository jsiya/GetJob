using GetJob.Models.DB;
using GetJob.Models.MenuModel;
using GetJob.Models.Notifications;
using MenuModel;

namespace GetJob.Models.AdminModel;
public class Admin: Member, IAuth
{
    public List<Notification> Notifications { get; set; }
    public Admin() { }
    public Admin(string email, string username, string password, List<Notification> notifications)
    {
        Id = Guid.NewGuid();
        Mail = email;
        Username = username;
        Password = password;
        Notifications = notifications;
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
                var user = db.Admins.Find(admin => admin.Username == username && admin.Password == password.GetPrivateString());
                if (user != null)
                {
                    this.Id = user.Id;
                    this.Username = user.Username;
                    this.Password = password.GetPrivateString();
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
    public override string ToString()
    {
        return $"id={Id}\nmail={Mail}\nusername={Username}\npassword={Password}";
    }
}

