using GetJob.Models.AdminModel;
using System.Text.Json;
using Newtonsoft.Json;
using GetJob.Models.VacancyModel;
using GetJob.Models.UserModels;

namespace GetJob.Models.DB;

public partial class Database
{
    public partial void Reader()
    {
        using FileStream fs = new FileStream("Admin.json", FileMode.Open);
        Admins = System.Text.Json.JsonSerializer.Deserialize<List<Admin>>(fs);
        if( Admins == null) 
            Admins = new();

        using FileStream fs2 = new FileStream("Employees.json", FileMode.Open);
        Employees = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(fs2);
        if (Employees == null)
            Employees = new();

        using FileStream fs3 = new FileStream("Employers.json", FileMode.Open);
        Employers = System.Text.Json.JsonSerializer.Deserialize<List<Employer>>(fs3);
        if (Employers == null)
            Employers = new();

        using FileStream fs4 = new FileStream("Categories.json", FileMode.Open);
        Categories = System.Text.Json.JsonSerializer.Deserialize<List<Category>>(fs4);
        if (Categories == null)
            Categories = new();

        using FileStream fs5 = new FileStream("ActiveVacancies.json", FileMode.Open);
        ActiveVacancies = System.Text.Json.JsonSerializer.Deserialize<List<Vacancy>>(fs5);
        if (ActiveVacancies == null)
            ActiveVacancies = new();

        using FileStream fs6 = new FileStream("DeactiveVacancies.json", FileMode.Open);
        DeactiveVacancies = System.Text.Json.JsonSerializer.Deserialize<List<Vacancy>>(fs6);
        if (DeactiveVacancies == null)
            DeactiveVacancies = new();

        using FileStream fs7 = new FileStream("ActiveResumes.json", FileMode.Open);
        ActiveResumes = System.Text.Json.JsonSerializer.Deserialize<List<Resume>>(fs7);
        if (ActiveResumes == null)
            ActiveResumes = new();

        using FileStream fs8 = new FileStream("DeactiveResumes.json", FileMode.Open);
        DeactiveResumes = System.Text.Json.JsonSerializer.Deserialize<List<Resume>>(fs8);
        if (DeactiveResumes == null)
            DeactiveResumes = new();

    }
    public partial void Writer()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        if (Admins != null)
            File.WriteAllText("Admin.json", System.Text.Json.JsonSerializer.Serialize(Admins, options));

        //Console.WriteLine(JsonSerializer.Serialize(list));
        File.WriteAllText("Employees.json", System.Text.Json.JsonSerializer.Serialize(Employees, options));

        File.WriteAllText("Employers.json", System.Text.Json.JsonSerializer.Serialize(Employers, options));

        File.WriteAllText("Categories.json", System.Text.Json.JsonSerializer.Serialize(Employers, options));

        File.WriteAllText("ActiveVacancies.json", System.Text.Json.JsonSerializer.Serialize(Employers, options));

        File.WriteAllText("DeactiveVacancies.json", System.Text.Json.JsonSerializer.Serialize(Employers, options));

        File.WriteAllText("ActiveResumes.json", System.Text.Json.JsonSerializer.Serialize(Employers, options));

        File.WriteAllText("DeactiveResumes.json", System.Text.Json.JsonSerializer.Serialize(Employers, options));

    }
}
