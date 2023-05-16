namespace GetJob.Models.VacancyModel;
public class Vacancy
{
    public Guid EmployerId { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateOnly DownloadDate { get; set; }
    public DateOnly ExpireDate { get; set; }
    public Category Category { get; set; }
    public int Payment { get; set; } 
    public string Description { get; set; }
    public bool IsActive { get; set; }//admin tesdiqleyene kimi deactive
    public bool Showable { get; set; }
    public int ViewCount { get; set; }
    public Vacancy() { }
    public List<Guid> Appliers { get; set; } //vakansiyalara apply olanlar?
    public Vacancy(Guid employerId, DateOnly downloadDate, DateOnly expireDate, Category category, int payment, string description, string title)
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
    }
}
