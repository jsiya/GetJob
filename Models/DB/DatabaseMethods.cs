using GetJob.Models.AdminModel;
using System.Text.Json;

namespace GetJob.Models.DB;

public partial class Database
{
    public partial void Reader()
    {
        using FileStream fs = new FileStream("Admin.json", FileMode.Open);
        Admins = System.Text.Json.JsonSerializer.Deserialize<List<Admin>>(fs);

        //using FileStream fs2 = new FileStream("Employees.json", FileMode.Open);
        //Employees = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(fs);

        //using FileStream fs3 = new FileStream("Employers.json", FileMode.Open);
        //Employers = System.Text.Json.JsonSerializer.Deserialize<List<Employer>>(fs);
        if( Admins == null) Admins = new();
        if(Employees == null) Employees = new();
        if(Employers == null) Employers = new();
        if(Categories == null) Categories = new();
        if(ActiveVacancies == null) ActiveVacancies = new();
        if(DeactiveVacancies == null) DeactiveVacancies = new();
        if(ActiveResumes == null) ActiveResumes = new();
        if(DeactiveResumes == null) DeactiveResumes = new();

    }
    public partial void Writer()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        if(Admins != null)
            File.WriteAllText("Admin.json", JsonSerializer.Serialize(Admins, options));
        //Console.WriteLine(JsonSerializer.Serialize(list));
        //File.WriteAllText("Employees.json", JsonSerializer.Serialize(Employees, options));

        //File.WriteAllText("Employers.json", JsonSerializer.Serialize(Employers, options));

    }
}
