using GetJob.Models.DB;
using GetJob.Models.UserModels;
using MenuModel;

namespace GetJob.Models.MenuModel;

public static class ResumeMenu
{
    public static void LanguageMenu( ref List<KeyValuePair<string, LanguageLevelInfo>> languages)
    {
        string language = "";
        while(language.Length == 0) 
        {
            Console.SetCursorPosition(33, 15);
            language = Console.ReadLine(); 
        }
        string[] ops = new string[] { "Beginner", "PreIntermediate", "Intermediate", "UpperIntermediate", "Advanced", "Expert" };
        Menu langMenu = new(ops, 12, Console.LargestWindowHeight);
        int choice = langMenu.RunMenu();
        if (choice == 0) languages.Add(new KeyValuePair<string, LanguageLevelInfo>(language, LanguageLevelInfo.Beginner));
        else if (choice == 1) languages.Add(new KeyValuePair<string, LanguageLevelInfo>(language, LanguageLevelInfo.PreIntermediate));
        else if (choice == 2) languages.Add(new KeyValuePair<string, LanguageLevelInfo>(language, LanguageLevelInfo.Intermediate));
        else if (choice == 3) languages.Add(new KeyValuePair<string, LanguageLevelInfo>(language, LanguageLevelInfo.UpperIntermediate));
        else if (choice == 3) languages.Add(new KeyValuePair<string, LanguageLevelInfo>(language, LanguageLevelInfo.Advanced));
        else if (choice == 3) languages.Add(new KeyValuePair<string, LanguageLevelInfo>(language, LanguageLevelInfo.Expert));
    }
    public static void CreateResumeMenu(ref Database db, ref Member user)
    {
        string profession = "", linkedin = "";
        List<string> educations = new List<string>();
        List<string> skills = new List<string>();
        List<string> certificates = new List<string>();
        List<KeyValuePair<string, LanguageLevelInfo>> languages = new();
        List<KeyValuePair<KeyValuePair<string, string>, KeyValuePair<string, string>>> experiences = new();
        List<string> options = new() { "Profession : ", "LinkedIn : ", "Education(many) : ", "Skills(many) : ", "Certificates(many) : ", "Languages(many) : ", "Experience(many) : ", "<<Create>>", "<==Back" };
        Menu menu = new(options.ToArray(), 10, 15);
        int choice;
        while (true)
        {
            Console.ResetColor();
            Console.Clear();
            Logo.ShowNewResumeLogo();
            menu._menuList[0] = $"Profession :  {profession}";
            menu._menuList[1] = $"Linkedin :  {linkedin}";
            if (educations.Count > 0)
                menu._menuList[2] = $"Educations(many) :  {educations.First()} ...";
            if (skills.Count > 0)
                menu._menuList[3] = $"Skills(many) :  {skills.First()}, ...";
            if (certificates.Count > 0)
                menu._menuList[4] = $"Certificates(many) :  {certificates.First()}, ...";
            if (languages.Count > 0)
                menu._menuList[5] = $"Languages(many) :  {languages.First().Key}, ...";
            if (experiences.Count > 0)
                menu._menuList[6] = $"Experiences(many) :  {experiences.First().Key} : {experiences.First().Value}, ...";
            choice = menu.RunMenu();
            if (choice == 0)
            {
                Console.SetCursorPosition(40, 10);
                while(profession.Length == 0) profession = Console.ReadLine();
            }
            else if (choice == 1)
            {
                Console.SetCursorPosition(33, 11);
                while (linkedin.Length == 0) linkedin = Console.ReadLine();
            }
            else if (choice == 2)
            {
                Console.SetCursorPosition(33, 12);
                string edu = "";
                while (edu.Length == 0) edu = Console.ReadLine();
                    educations.Add(edu);
            }
            else if (choice == 3)
            {
                Console.SetCursorPosition(33, 13); 
                skills.Add(Console.ReadLine());
            }
            else if (choice == 4)
            {
                Console.SetCursorPosition(33, 14);
                string certificate = "";
                while (certificate.Length == 0) certificate = Console.ReadLine();
                certificates.Add(certificate);
            }
            else if (choice == 5)
            {
                LanguageMenu(ref languages);
            }
            else if (choice == 6)
            {
                string company, position, begin, end;
                Console.SetCursorPosition(72, 11);
                Console.WriteLine("Company: ");
                Console.SetCursorPosition(72, 12);
                Console.WriteLine("Position: ");
                Console.SetCursorPosition(72, 13);
                Console.WriteLine("Begining year: ");
                Console.SetCursorPosition(72, 14);
                Console.WriteLine("Ending year: ");
                Console.SetCursorPosition(89, 11);
                company = Console.ReadLine();
                Console.SetCursorPosition(89, 12);
                position = Console.ReadLine();
                Console.SetCursorPosition(89, 13);
                begin = Console.ReadLine();
                Console.SetCursorPosition(89, 14);
                end = Console.ReadLine();
                experiences.Add(new KeyValuePair<KeyValuePair<string, string>, KeyValuePair<string, string>>(new KeyValuePair<string, string>(company, position), new KeyValuePair<string, string>(begin,end)));
            }
            else if (choice == 7)
            {
                Employee employee = user as Employee;
                Resume resume = new Resume(user.Id, profession, $"{employee.Name} {employee.Surname}", linkedin, educations, skills, languages, certificates, experiences);
                employee.Resumes.Add(resume);
                db.DeactiveResumes.Add(resume);
                db.Writer();
                break;
            }
            if (choice == 8) break;
        }
    }
    public static void ShowUsersResumesMenu(ref Database db, ref Member user)
    {
        Console.Clear();
        Logo.ShowResumesLogo();
        Employee employee = user as Employee;
        List<string> options = new() { "<=Back" };
        List<Resume> resumes = employee.Resumes;
        if (resumes != null)
        {
            options.AddRange(resumes.Select(resume => resume.ToString()));
        }
        Menu menu = new Menu(options.ToArray(), 12, Console.LargestWindowHeight);
        while (true)
        {
            int choice = menu.RunMenu();
            if (choice == 0) break;
        }
    }

    public static void TransferResumes(ref Database db)
    {
        db.ActiveResumes.AddRange(db.DeactiveResumes.Where(vacancy => vacancy.IsActive == true));
        db.DeactiveResumes.RemoveAll(vacancy => vacancy.IsActive == true);
        db.DeactiveResumes.AddRange(db.ActiveResumes.Where(vac => vac.IsActive == false));
        db.ActiveResumes.RemoveAll(vac => vac.IsActive == false);
        db.Writer();
    }
}
