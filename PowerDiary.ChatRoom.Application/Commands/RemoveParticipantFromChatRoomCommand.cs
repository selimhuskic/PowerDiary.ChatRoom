using MediatR;
using PowerDiary.ChatRoom.Application.Repositories.Interfaces;
using PowerDiary.ChatRoom.Infrastructure.Models;

namespace PowerDiary.ChatRoom.Infrastructure.Commands
{
    public record RemoveParticipantFromChatRoomCommand(
        Guid ChatRoomId, 
        string ParticipantName,
        DateTime At) : IRequest;

    public class RemoveParticipantFromChatRoomCommandHandler(IChatRoomRepository chatRoomRepository)
        : IRequestHandler<RemoveParticipantFromChatRoomCommand>
    {
        private readonly IChatRoomRepository _chatRoomRepository = chatRoomRepository;

        public Task Handle(RemoveParticipantFromChatRoomCommand request, CancellationToken cancellationToken)
        {
            var chatRoom = _chatRoomRepository.GetById(request.ChatRoomId);

            if (chatRoom == null)
                return Task.FromResult(false);

            chatRoom.RemoveParticipant(request.ParticipantName);

            var chatRoomEvent = new Event(EventType.LeaveTheRoom, GetContent(request, request.At), request.At);

            _chatRoomRepository.UpsertChatRoom(chatRoom, chatRoomEvent);

            return Task.CompletedTask;
        }

        private static string GetContent(RemoveParticipantFromChatRoomCommand request, DateTime at) =>
            $"{at} {request.ParticipantName} leaves the room";
    }
}
