using GetJob.Models.AdminModel;
using GetJob.Models.UserModels;
using GetJob.Models.VacancyModel;

namespace GetJob.Models.DB;

public partial class Database
{
    public int Id { get; set; }
    public List<Employee> Employees { get; set; }
    public List<Employer> Employers { get; set; }
    public List<Admin> Admins { get; set; }
    public List<Category> Categories { get; set; }
    public List<Vacancy> Vacancies { get; set; }
    public List<Resume> Resumes { get; set; }

    public partial void Reader();
    public partial void Writer();

}
