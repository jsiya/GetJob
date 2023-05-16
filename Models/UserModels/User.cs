using GetJob.Models.Notifications;

namespace GetJob.Models.UserModels;

public abstract class User: Member
{
    public int Age { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string City { get; set; }
    public string Phone { get; set; }
    public int ViewCount { get; set; }
    public List<Notification> Notifications { get; set; }
}
