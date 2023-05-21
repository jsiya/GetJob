using GetJob.Models.DB;
using GetJob.Models.VacancyModel;
using MenuModel;

namespace GetJob.Models.MenuModel;

public static class VacancyMenu
{
    //vakansiya yaratma menusu
    public static void CreateVacancyMenu(ref Database db, ref Member user)
    {
        Employer employer1 = new Employer();
        string title = "", description = "";
        int payment = 0;
        Category category = new Category();
        category.Name = "";
        List<string> options = new() { "Title : ", "Description : ", "Catagegory : ", "Payment : ", "<<Create>>", "<==Back" };
        Menu menu = new(options.ToArray(), 10, 15);
        int choice;
        while (true)
        {
            Console.ResetColor();
            Console.Clear();
            Logo.ShowNewVacancyLogo();
            menu._menuList[0] = $"Title :  {title}";
            menu._menuList[1] = $"Description :  {description}";
            menu._menuList[2] = $"category :  {category.Name}";
            menu._menuList[3] = $"Payment :  {payment}";
            choice = menu.RunMenu();
            if (choice == 0)
            {
                Console.SetCursorPosition(40, 10);
                title = "";
                while (title.Length == 0) title = Console.ReadLine();
            }
            else if (choice == 1)
            {
                description = "";
                Console.SetCursorPosition(40, 11);
                while (description.Length == 0) description = Console.ReadLine();
            }
            else if (choice == 2)
            {
                Console.SetCursorPosition(40, 12);
                category = CategoryMenues.CategoryOptions(db.Categories);
            }
            else if (choice == 3)
            {
                Console.SetCursorPosition(40, 13);
                while (!int.TryParse(Console.ReadLine(), out payment)) ;
            }
            else if (choice == 4)
            {
                Employer employer = user as Employer;
                Vacancy vacancy = new Vacancy(employer.Id, DateTime.Now.Date, DateTime.Now.Date.AddDays(30), category, payment, description, title);
                employer.Vacancies.Add(vacancy);
                db.DeactiveVacancies.Add(vacancy);
                db.Writer();
                employer.Notifications.Add(new Notifications.Notification("New Vacancy Created!", DateTime.Now.ToString(), user));
                break;
            }
            if (choice == 5) break;
        }
    }

    //butun vakansiyalar
    public static void AllVacanciesMenu(ref Database db, ref Member user) 
    {
        List<string> vacs = new() { "<=back" };
        vacs.AddRange(db.ActiveVacancies.Where(vacancy => vacancy.Showable).Select(vacancy => vacancy.Title).ToList());

        Menu menu = new Menu(vacs.ToArray(), 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.Clear();
            Console.ResetColor();
            Logo.ShowVacanciesLogo();
            choice = menu.RunMenu();
            if (choice == 0) break;
            Console.Clear();
            Console.WriteLine(db.ActiveVacancies.ElementAt(choice - 1).ToString());
            if (user is Employee)//user gelibse vakansiyaya baxib apply etsin
            {
                Console.WriteLine("Do you want to apply?");
                Menu suggestMenu = new Menu(new string[] { "Yes", "No" }, 12, 20);
                int choice2 = suggestMenu.RunMenu();
                if (choice2 == 0)
                {
                    db.ActiveVacancies.ElementAt(choice - 1).ViewCount++;
                    //eger user evvel apply olmayibsa apply edir
                    if (!db.ActiveVacancies.ElementAt(choice - 1).Appliers.Contains(user.Id))
                    {
                        db.ActiveVacancies.ElementAt(choice - 1).Appliers.Add(user.Id);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Applied!");
                        Thread.Sleep(2000);
                        db.Writer();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("You already aplied to this vacancy!");
                        Thread.Sleep(2000);
                    }
                }
            }
            else if(user is Employer)
            {
                if(user.Id != db.ActiveVacancies.ElementAt(choice - 1).EmployerId)
                {
                    Console.WriteLine("Press any key to return...");
                    db.ActiveVacancies.ElementAt(choice - 1).ViewCount++;
                    var key = Console.ReadKey();
                }
                else
                {
                    EmployerMenues.ApplyEmployeeMenu(ref db, ref user, choice - 1);
                }
            }
        }
    }

    public static void ShowUsersVacanciesMenu(ref Database db, ref Member user)//employerin oz vakansiyalari
    {
        Console.Clear();
        Logo.ShowVacanciesLogo();
        Employer employer = user as Employer;
        List<string> options = new() { "<=Back" };
        List<Vacancy> vacancies = employer.Vacancies;
        if (vacancies != null)
        {
            options.AddRange(vacancies.Select(vacancy => vacancy.ToString()));
        }
        Menu menu = new Menu(options.ToArray(), 12, Console.LargestWindowHeight);
        while (true)
        {
            int choice = menu.RunMenu();
            if (choice == 0) break;
        }
    }

    public static void TransferVacancies(ref Database db)
    {
        db.ActiveVacancies.AddRange(db.DeactiveVacancies.Where(vacancy => vacancy.IsActive == true));
        db.DeactiveVacancies.RemoveAll(vacancy => vacancy.IsActive == true);
        db.DeactiveVacancies.AddRange(db.ActiveVacancies.Where(vac => vac.IsActive == false));
        db.ActiveVacancies.RemoveAll(vac => vac.IsActive == false);
        db.Writer();    
    }
}
