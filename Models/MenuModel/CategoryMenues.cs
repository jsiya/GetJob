using GetJob.Models.AdminModel;
using GetJob.Models.DB;
using GetJob.Models.VacancyModel;
using MenuModel;

namespace GetJob.Models.MenuModel;

public static class CategoryMenues
{
    //admin categorylere baxsin deye
    public static Category CategoryMenu(List<Category> categories) 
    {
        List<string> ops = new() { "<=back" };
        ops.AddRange(categories.Select(c => c.Name).ToList());
        Menu categoryMenu = new(ops.ToArray(), 12, Console.LargestWindowHeight);
        Console.Clear();
        Logo.ShowCategoryLogo();
        int choice = categoryMenu.RunMenu();
        if (choice == 0) return new();
        Category category = categories[choice];
        return category;
    }

    //vakansiya yaratdiqda uygun vakansiya secim menyusu
    public static Category CategoryOptions(List<Category> categories)
    {
        List<string> ops = categories.Select(c => c.Name).ToList();
        Menu categoryMenu = new(ops.ToArray(), 12, Console.LargestWindowHeight);
        Console.Clear();
        Logo.ShowCategoryLogo();
        int choice = categoryMenu.RunMenu();
        Category category = categories[choice];
        return category;
    }

    //admin baxsin ve ya yaratsin
    public static void Categories(ref Database db, ref Member member) 
    {
        List<string> options = new() { "All Category", "<<Create New>>", "<==Back" };
        Menu menu = new(options.ToArray(), 12, Console.LargestWindowHeight);
        int choice;
        while (true)
        {
            Console.ResetColor();
            Console.Clear();
            Logo.ShowCategoryLogo();
            choice = menu.RunMenu();
            if (choice == 0) CategoryMenu(db.Categories);

            else if (choice == 1) CreateCategoryMenu(ref db, ref member);

            else break;
        }
    }

    //admin ucun yeni category yaratma
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
            Logo.ShowCategoryLogo();
            menu._menuList[0] = $"Category Name :  {title}";
            choice = menu.RunMenu();
            if (choice == 0)
            {
                while (title.Length == 0) 
                {
                    Console.SetCursorPosition(75, 12); 
                    title = Console.ReadLine();
                }
            }
            else if (choice == 1)//create secende
            {
                Admin employer = member as Admin;
                Category category = new Category(title);
                db.Categories.Add(category);
                db.Writer();
                break;
            }
            //yaratmadan geri qayitma
            if (choice == 2) break;
        }
    }
}
