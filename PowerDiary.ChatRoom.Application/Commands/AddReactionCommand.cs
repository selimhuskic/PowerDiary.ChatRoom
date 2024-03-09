using MediatR;
using PowerDiary.ChatRoom.Application.Extensions;
using PowerDiary.ChatRoom.Application.Repositories.Interfaces;
using PowerDiary.ChatRoom.Domain.Enums;
using PowerDiary.ChatRoom.Domain.Models;
using PowerDiary.ChatRoom.Infrastructure.Models;

namespace PowerDiary.ChatRoom.Infrastructure.Commands
{
    public record AddReactionCommand(
        Guid ChatRoomId, 
        string ParticipantName, 
        string OtherParticipantName, 
        int ReactionType,
        DateTime At) : IRequest<bool>;

    public class AddReactionCommandHandler(IChatRoomRepository chatRoomRepository) 
        : IRequestHandler<AddReactionCommand, bool>
    {
        private readonly IChatRoomRepository _chatRoomRepository = chatRoomRepository;

        public Task<bool> Handle(AddReactionCommand request, CancellationToken cancellationToken)
        {
            var chatRoom = _chatRoomRepository.GetById(request.ChatRoomId);

            if (chatRoom == null)
                return Task.FromResult(false);

            var reactionType = (ReactionType)request.ReactionType;

            chatRoom.AddReaction(request.ParticipantName, new Reaction(request.OtherParticipantName, reactionType));

            var chatRoomEvent = new Event(reactionType, GetContent(request, reactionType, request.At), request.At);

            _chatRoomRepository.UpsertChatRoom(chatRoom, chatRoomEvent);

            return Task.FromResult(true);
        }

        private static string GetContent(AddReactionCommand request, ReactionType reactionType, DateTime at) =>
            $"{at} {request.ParticipantName} {reactionType.ReactionTypeFriendlyOutput()} to {request.OtherParticipantName}";
    }
}
