using PowerDiary.ChatRoom.Domain.Enums;

namespace PowerDiary.ChatRoom.Domain.ValueObjects
{
    public sealed class Reaction(
        string otherParticipantName, 
        ReactionType reactionType,
        DateTime postedAt)
    {
        public Participant OtherParticipant { get; } = new Participant(otherParticipantName);
        public ReactionType ReactionType { get; } = reactionType;
        public DateTime PostedAt { get; } = postedAt;
    }
}
