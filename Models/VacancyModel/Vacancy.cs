namespace GetJob.Models.VacancyModel;
public class Vacancy
{
    public Guid EmployerId { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime DownloadDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public Category Category { get; set; }
    public int Payment { get; set; } 
    public string Description { get; set; }
    public bool IsActive { get; set; }//admin tesdiqleyene kimi deactive
    public bool Showable { get; set; }
    public int ViewCount { get; set; }
    public List<Guid> Appliers { get; set; } //vakansiyalara apply edenler
    public Vacancy() { }

    public Vacancy(Guid employerId, DateTime downloadDate, DateTime expireDate, Category category, int payment, string description, string title)
    {
        EmployerId = employerId;
        Id = Guid.NewGuid();
        DownloadDate = downloadDate;
        ExpireDate = expireDate;
        Category = category;
        Payment = payment;
        Description = description;
        IsActive = false;
        Title = title;
        Appliers = new List<Guid>();
    }

    public override string ToString()
    {
        return $@"
                       -Vacancy-
Title: {Title}, 
Category: {Category.Name}, 
Description: {Description}, 
Payment: {Payment}
";
    }

    public string ToStringForAdmin()
    {
        return $@"
                        -Vacancy-
Job Title: {Title}
Category: {Category.Name}
Description: {Description}
Payment: {Payment}
IsActive: {(IsActive?"Active":"Deactive")}
";
    }
}
