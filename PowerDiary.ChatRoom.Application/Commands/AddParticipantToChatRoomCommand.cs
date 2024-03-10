using MediatR;
using PowerDiary.ChatRoom.Application.Repositories.Interfaces;
using PowerDiary.ChatRoom.Application.Models;

namespace PowerDiary.ChatRoom.Application.Commands
{
    public record AddParticipantToChatRoomCommand(
        Guid ChatRoomId, 
        string ParticipantName,
        DateTime At) : IRequest<bool>;

    public class AddParticipantToChatRoomCommandHandler(IChatRoomRepository chatRoomRepository) 
        : IRequestHandler<AddParticipantToChatRoomCommand, bool>
    {
        private readonly IChatRoomRepository _chatRoomRepository = chatRoomRepository;

        public Task<bool> Handle(AddParticipantToChatRoomCommand request, CancellationToken cancellationToken)
        {
            var chatRoom = _chatRoomRepository.GetById(request.ChatRoomId);

            if (chatRoom == null)
                return Task.FromResult(false);

            chatRoom.AddParticipant(request.ParticipantName);

            var chatRoomEvent = new Event(EventType.EnterTheRoom, GetContent(request.ParticipantName, request.At), request.At);

            _chatRoomRepository.UpsertChatRoom(chatRoom, chatRoomEvent);

            return Task.FromResult(true);
        }

        private static string GetContent(string participantName, DateTime at) =>
            $"{at} {participantName} enters the room";
    }
}
