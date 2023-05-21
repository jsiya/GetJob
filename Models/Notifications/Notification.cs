using GetJob.Models.UserModels;
namespace GetJob.Models.Notifications;
public class Notification
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public DateTime NotificationDateTime { get; set; }
    public Member? FromUser { get; set; }
    public Notification() { }
    public Notification(string text, DateTime notificationDateTime, Member fromUser)
    {
        Id = Guid.NewGuid();
        Text = text;
        NotificationDateTime = notificationDateTime;
        FromUser = fromUser;
    }
    public override string ToString()
    {
        return $" Action: {Text} \n by {FromUser?.Username}, {NotificationDateTime}";
    }
}
