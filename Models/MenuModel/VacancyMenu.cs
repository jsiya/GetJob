using GetJob.Models.DB;
using GetJob.Models.VacancyModel;
using MenuModel;

namespace GetJob.Models.MenuModel;

public static class VacancyMenu
{
    //vakansiya yaratma menusu
    public static void CreateVacancyMenu(ref Database db, ref Member user)
    {
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
                break;
            }
            if (choice == 5) break;
        }
    }

    //butun vakansiyalar
    public static void AllVacanciesMenu(ref Database db, ref Member user)
    {
        List<string> users = db.ActiveVacancies.Where(vacancy => vacancy.Showable).Select(vacancy => vacancy.Title).ToList();
        users.Add("<=Back");

        Menu menu = new Menu(users.ToArray(), 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.Clear();
            Logo.ShowVacanciesLogo();
            choice = menu.RunMenu();
            if (choice == users.Count - 1) break;
        }
    }

    public static void ShowUsersVacanciesMenu(ref Database db, ref Member user)
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
