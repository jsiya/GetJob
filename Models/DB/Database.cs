using GetJob.Models.AdminModel;
using GetJob.Models.UserModels;
using GetJob.Models.VacancyModel;

namespace GetJob.Models.DB;

public partial class Database
{
    public int Id { get; set; }
    public List<Employee> Employees { get; set; }  //Isciler
    public List<Employer> Employers { get; set; } //Is verenler
    public List<Admin> Admins { get; set; } //adminler
    public List<Category> Categories { get; set; } //vakansiya kateqoriyalari
    public List<Vacancy> ActiveVacancies { get; set; } //aktiv olan butun vakansiyalar
    public List<Vacancy> DeactiveVacancies { get; set; }//deaktiv vakansiyalar
    public List<Resume> ActiveResumes { get; set; }//aktiv cv-ler
    public List<Resume> DeactiveResumes { get; set; }//deaktiv cv-ler

    public partial void Reader();
    public partial void Writer();

}
