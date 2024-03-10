namespace PowerDiary.ChatRoom.Domain.ValueObjects
{
    public sealed record Comment(
        string Content, 
        DateTime PostedAt);
}
