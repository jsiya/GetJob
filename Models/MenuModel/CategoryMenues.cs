using GetJob.Models.AdminModel;
using GetJob.Models.DB;
using GetJob.Models.VacancyModel;
using MenuModel;

namespace GetJob.Models.MenuModel;

public static class CategoryMenues
{
    public static Category CategoryMenu(List<Category> categories) 
    {
        List<string> ops = categories.Select(c => c.Name).ToList();
        ops.Add("<=Back");
        Menu categoryMenu = new(ops.ToArray(), 12, Console.LargestWindowHeight);
        int choice = categoryMenu.RunMenu();
        if (choice == ops.Count - 10) return new();
        Category category = categories[choice];
        return category;
    }

    public static Category CategoryOptions(List<Category> categories)
    {
        List<string> ops = categories.Select(c => c.Name).ToList();
        Menu categoryMenu = new(ops.ToArray(), 12, Console.LargestWindowHeight);
        int choice = categoryMenu.RunMenu();
        Category category = categories[choice];
        return category;
    }

    public static void Categories(ref Database db, ref Member member)  //admin ucun olan
    {
        List<string> options = new() { "All Category", "<<Create New>>", "<==Back" };
        Menu menu = new(options.ToArray(), 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.ResetColor();
            Console.Clear();
            Logo.ShowLogo();
            choice = menu.RunMenu();
            if (choice == 0) CategoryMenu(db.Categories);

            else if (choice == 1) CreateCategoryMenu(ref db, ref member);

            else break;
        }
    }

    public static void CreateCategoryMenu(ref Database db, ref Member member)
    {
        string title = "";
        List<string> options = new() { "Category Name : ", "<<Create>>", "<==Back" };
        Menu menu = new(options.ToArray(), 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.ResetColor();
            Console.Clear();
            Logo.ShowNewResumeLogo();
            menu._menuList[0] = $"Category Name :  {title}";
            choice = menu.RunMenu();
            if (choice == 0)
            {
                Console.SetCursorPosition(75, 12);
                while (title.Length == 0) title = Console.ReadLine();
            }
            else if (choice == 1)
            {
                Admin employer = member as Admin;
                Category category = new Category(title);
                db.Categories.Add(category);
                db.Writer();
                break;
            }
            if (choice == 2) break;
        }
    }
}
