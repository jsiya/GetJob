namespace GetJob.Models.VacancyModel;
public record struct Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Category(string name)
    {
        Id = new Guid();
        Name = name;
    }
}
