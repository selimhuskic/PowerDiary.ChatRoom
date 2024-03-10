using PowerDiary.ChatRoom.Domain.Enums;

namespace PowerDiary.ChatRoom.Domain.ValueObjects
{
    public class Reaction(string otherParticipantName, ReactionType reactionType)
    {
        public Participant OtherParticipant { get; } = new Participant(otherParticipantName);
        public ReactionType ReactionType { get; } = reactionType;
    }
}
