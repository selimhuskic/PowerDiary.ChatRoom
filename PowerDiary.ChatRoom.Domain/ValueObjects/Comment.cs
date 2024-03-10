namespace PowerDiary.ChatRoom.Domain.ValueObjects
{
    public record Comment(
        string Content, 
        DateTime PostedAt);
}
