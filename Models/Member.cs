namespace GetJob.Models;
//evvel abstract yazmisdim ama jsondan ouya bilmir deye deyisirem
public class Member
{
    public Guid Id { get; set; }
    public string Mail { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
