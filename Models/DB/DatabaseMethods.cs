using GetJob.Models.AdminModel;

namespace GetJob.Models.DB;

public partial class Database
{
    public partial void Reader()
    {
        using FileStream fs = new FileStream("Admin.json", FileMode.Open);
        Admins = System.Text.Json.JsonSerializer.Deserialize<List<Admin>>(fs);

        //using FileStream fs2 = new FileStream("Employee.json", FileMode.Open);
        //Employees = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(fs);

        //using FileStream fs3 = new FileStream("Employer.json", FileMode.Open);
        //Employers = System.Text.Json.JsonSerializer.Deserialize<List<Employer>>(fs);
    }
    public partial void Writer()
    {

    }
}
