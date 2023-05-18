using GetJob.Models.DB;
using GetJob.Models.UserModels;
using GetJob.Models.VacancyModel;
using MenuModel;

namespace GetJob.Models.MenuModel;

public static class VacancyMenu
{
    //vakansiya yaratma menusu
    public static void CreateVacancyMenu(ref Database db, ref Member user)
    {

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
}
