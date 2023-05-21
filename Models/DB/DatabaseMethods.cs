using GetJob.Models.AdminModel;
using GetJob.Models.UserModels;
using GetJob.Models.VacancyModel;
using System.Text.Json;

namespace GetJob.Models.DB;

public partial class Database
{
    private void SortDB()
    {
        Employees.OrderByDescending(employee => employee.ViewCount);
        Employers.OrderByDescending(employer => employer.ViewCount);
        ActiveResumes.OrderByDescending(resume => resume.ViewCount);
        ActiveVacancies.OrderByDescending(vacancy => vacancy.ViewCount);
    }
    public partial void Reader()
    {
        using FileStream fs = new FileStream("Admin.json", FileMode.Open);
        if (fs.Length > 0)
            Admins = System.Text.Json.JsonSerializer.Deserialize<List<Admin>>(fs);
        if( Admins == null) 
            Admins = new();

        using FileStream fs2 = new FileStream("Employees.json", FileMode.Open);
        if (fs2.Length > 0)
            Employees = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(fs2);
        if (Employees == null)
            Employees = new();

        using FileStream fs3 = new FileStream("Employers.json", FileMode.Open);
        if (fs3.Length > 0)
            Employers = System.Text.Json.JsonSerializer.Deserialize<List<Employer>>(fs3);
        if (Employers == null)
            Employers = new();

        using FileStream fs4 = new FileStream("Categories.json", FileMode.Open);
        if(fs4.Length > 0)
            Categories = System.Text.Json.JsonSerializer.Deserialize<List<Category>>(fs4);
        if (Categories == null)
            Categories = new();

        using FileStream fs5 = new FileStream("ActiveVacancies.json", FileMode.Open);
        if (fs5.Length > 0)
            ActiveVacancies = System.Text.Json.JsonSerializer.Deserialize<List<Vacancy>>(fs5);
        if (ActiveVacancies == null)
            ActiveVacancies = new();

        using FileStream fs6 = new FileStream("DeactiveVacancies.json", FileMode.Open);
        if (fs6.Length > 0)
            DeactiveVacancies = System.Text.Json.JsonSerializer.Deserialize<List<Vacancy>>(fs6);
        if (DeactiveVacancies == null)
            DeactiveVacancies = new();

        using FileStream fs7 = new FileStream("ActiveResumes.json", FileMode.Open);
        if (fs7.Length > 0)
            ActiveResumes = System.Text.Json.JsonSerializer.Deserialize<List<Resume>>(fs7);
        if (ActiveResumes == null)
            ActiveResumes = new();

        using FileStream fs8 = new FileStream("DeactiveResumes.json", FileMode.Open);
        if (fs8.Length > 0)
            DeactiveResumes = System.Text.Json.JsonSerializer.Deserialize<List<Resume>>(fs8);
        if (DeactiveResumes == null)
            DeactiveResumes = new();

    }
    public partial void Writer()
    {
        SortDB();
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        if (Admins != null)
            File.WriteAllText("Admin.json", System.Text.Json.JsonSerializer.Serialize(Admins, options));

        //Console.WriteLine(JsonSerializer.Serialize(list));
        File.WriteAllText("Employees.json", System.Text.Json.JsonSerializer.Serialize(Employees, options));

        File.WriteAllText("Employers.json", System.Text.Json.JsonSerializer.Serialize(Employers, options));

        File.WriteAllText("Categories.json", System.Text.Json.JsonSerializer.Serialize(Categories, options));

        File.WriteAllText("ActiveVacancies.json", System.Text.Json.JsonSerializer.Serialize(ActiveVacancies, options));

        File.WriteAllText("DeactiveVacancies.json", System.Text.Json.JsonSerializer.Serialize(DeactiveVacancies, options));

        File.WriteAllText("ActiveResumes.json", System.Text.Json.JsonSerializer.Serialize(ActiveResumes, options));

        File.WriteAllText("DeactiveResumes.json", System.Text.Json.JsonSerializer.Serialize(DeactiveResumes, options));

    }
}
