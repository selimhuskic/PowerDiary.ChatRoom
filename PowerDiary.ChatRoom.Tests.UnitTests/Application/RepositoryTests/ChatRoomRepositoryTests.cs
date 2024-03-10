using PowerDiary.ChatRoom.Application.Repositories;
using Xunit;

namespace PowerDiary.ChatRoom.Tests.UnitTests.Application.RepositoryTests
{
    public class ChatRoomRepositoryTests
    {
        private readonly ChatRoomRepository _chatRoomRepository;

        public ChatRoomRepositoryTests()
        {
            _chatRoomRepository = new ChatRoomRepository();
        }

        [Fact]
        public void GivenChatRoomCreatedAndUpserted_WhenGetByIdInvokedUsingItsId_ThenSameChatRoomReturned()
        {
            // Given
            var chatRoom = new ChatRoom.Domain.Aggregates.ChatRoom();
            chatRoom = _chatRoomRepository.UpsertChatRoom(chatRoom);

            // When
            var chatRoomFetchedById = _chatRoomRepository.GetById(chatRoom.Id);

            // Then
            Assert.Equal(chatRoom, chatRoomFetchedById);
        }

        [Fact]
        public void GivenChatRoomCreatedAndUpsertedWithEvents_WhenGetChatRoomEvents_ThenEventsReturned()
        {
            // Given
            var chatRoom = new ChatRoom.Domain.Aggregates.ChatRoom();

            chatRoom = _chatRoomRepository
                .UpsertChatRoom(chatRoom, 
                                new ChatRoom.Application.Models.Event(
                                    ChatRoom.Application.Models.EventType.Comment, 
                                    "content", 
                                    DateTime.UtcNow));
            chatRoom = _chatRoomRepository
                .UpsertChatRoom(chatRoom,
                                new ChatRoom.Application.Models.Event(
                                    ChatRoom.Application.Models.EventType.HighFive,
                                    "content",
                                    DateTime.UtcNow));

            // When
            var events = _chatRoomRepository.GetChatRoomEvents(chatRoom.Id);

            // Then
            Assert.Equal(2, events!.Count());
            Assert.Equal(1, events!.Count(e => e.ChatRoomEvent == ChatRoom.Application.Models.EventType.Comment));
            Assert.Equal(1, events!.Count(e => e.ChatRoomEvent == ChatRoom.Application.Models.EventType.HighFive));
        }
    }
}
