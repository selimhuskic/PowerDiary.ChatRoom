using PowerDiary.ChatRoom.Domain.Enums;

namespace PowerDiary.ChatRoom.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string ReactionTypeFriendlyOutput(this ReactionType reactionType)
        {
            switch (reactionType)
            {
                case ReactionType.HighFive:
                    return "high five";

                default:
                    throw new ArgumentException($"Could not parse {reactionType}");
            }
        }
    }
}
