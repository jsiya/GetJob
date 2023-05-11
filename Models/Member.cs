namespace GetJob.Models;
public abstract class Member
{
    public Guid Id { get; set; }
    public string Mail { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
