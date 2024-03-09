using PowerDiary.ChatRoom.Application.Repositories.Interfaces;
using PowerDiary.ChatRoom.Infrastructure.Models;

namespace PowerDiary.ChatRoom.Application.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private static Domain.Aggregates.ChatRoom? _mockedWriteSideChatRoom;
        private static Dictionary<Guid, List<Event>>? _mockedReadSideChatRoom;

        public Domain.Aggregates.ChatRoom? GetById(Guid chatRoomId) =>
              _mockedWriteSideChatRoom?.Id == chatRoomId ?
                    _mockedWriteSideChatRoom : null;

        // In a full implementation, here's where we would update the
        // WRITE and READ tables/databases within a single transaction

        public Domain.Aggregates.ChatRoom UpsertChatRoom(Domain.Aggregates.ChatRoom chatRoom, Event? chatRoomEvent = null)
        {
            _mockedWriteSideChatRoom = chatRoom;

            _mockedReadSideChatRoom ??= [];

            var isEventSent = chatRoomEvent != null;

            if (!isEventSent)
                return _mockedWriteSideChatRoom;

            var isFirstEvent = chatRoomEvent != null && !_mockedReadSideChatRoom.ContainsKey(chatRoom.Id);

            if (isFirstEvent)
            {
                _mockedReadSideChatRoom.Add(chatRoom.Id, [chatRoomEvent!]);
                return _mockedWriteSideChatRoom;
            }

            _mockedReadSideChatRoom[chatRoom.Id].Add(chatRoomEvent!);

            return _mockedWriteSideChatRoom;
        }

        public IEnumerable<Event>? GetChatRoomEvents(Guid chatRoomId) =>
            _mockedReadSideChatRoom?[chatRoomId];
    }
}
