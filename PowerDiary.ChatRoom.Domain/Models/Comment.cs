namespace PowerDiary.ChatRoom.Domain.Models
{
    public record Comment(
        string Content, 
        DateTime PostedAt);
}
