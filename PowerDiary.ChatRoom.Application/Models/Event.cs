using PowerDiary.ChatRoom.Domain.Enums;
using System.Text.Json.Serialization;

namespace PowerDiary.ChatRoom.Infrastructure.Models
{
    public class Event
    {
        public EventType ChatRoomEvent { get; }

        public string Content { get; }

        public DateTime At { get; }

        public Event(EventType eventType, string content, DateTime at) =>
            (ChatRoomEvent, Content, At) = (eventType, content, at);

        public Event(ReactionType reactionType, string content, DateTime at) =>        
            (ChatRoomEvent, Content, At) = (GetEventTypeBasedOnReaction(reactionType), content, at);       


        private static EventType GetEventTypeBasedOnReaction(ReactionType reactionType)
        {
            return reactionType switch
            {
                ReactionType.HighFive => EventType.HighFive,
                _ => throw new ArgumentOutOfRangeException("Reaction type not associated with an event!"),
            };
        }
    }    

    public enum EventType
    {
        [JsonPropertyName("enter-the-room")]
        EnterTheRoom = 10,

        [JsonPropertyName("leave-the-room")]
        LeaveTheRoom = 11,

        [JsonPropertyName("comment")]
        Comment = 20,
        [JsonPropertyName("high-five")]
        HighFive = 21
    }
}
