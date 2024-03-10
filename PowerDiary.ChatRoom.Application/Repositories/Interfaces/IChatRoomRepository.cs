using PowerDiary.ChatRoom.Application.Models;

namespace PowerDiary.ChatRoom.Application.Repositories.Interfaces
{
    public interface IChatRoomRepository
    {
        Domain.Aggregates.ChatRoom? GetById(Guid chatRoomId);

        Domain.Aggregates.ChatRoom UpsertChatRoom(Domain.Aggregates.ChatRoom chatRoom, Event? chatRoomEvent = null);

        IEnumerable<Event>? GetChatRoomEvents(Guid chatRoomId);
    }
}
