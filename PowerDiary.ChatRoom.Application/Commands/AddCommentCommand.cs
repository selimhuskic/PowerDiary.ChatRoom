using MediatR;
using PowerDiary.ChatRoom.Application.Repositories.Interfaces;
using PowerDiary.ChatRoom.Domain.Models;
using PowerDiary.ChatRoom.Infrastructure.Models;

namespace PowerDiary.ChatRoom.Infrastructure.Commands
{
    public record AddCommentCommand(
        Guid ChatRoomId, 
        string ParticipantName, 
        string Comment,
        DateTime At) : IRequest<bool>;

    public class AddCommentCommandHandler(IChatRoomRepository chatRoomRepository) 
        : IRequestHandler<AddCommentCommand, bool>
    {
        private readonly IChatRoomRepository _chatRoomRepository = chatRoomRepository;

        public Task<bool> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var chatRoom = _chatRoomRepository.GetById(request.ChatRoomId);

            if (chatRoom == null)
                return Task.FromResult(false);

            chatRoom.AddCommment(request.ParticipantName, new Comment(request.Comment, request.At));

            var chatRoomEvent = new Event(EventType.Comment, GetContent(request, request.At), request.At);

            _chatRoomRepository.UpsertChatRoom(chatRoom, chatRoomEvent);

            return Task.FromResult(true);            
        }

        private static string GetContent(AddCommentCommand request, DateTime at) =>
                $"{at} {request.ParticipantName} comments: {request.Comment}";
    }
}
