using MediatR;
using PowerDiary.ChatRoom.Application.Repositories.Interfaces;

namespace PowerDiary.ChatRoom.Infrastructure.Commands
{
    public record CreateChatRoomCommand() : IRequest<Guid>;

    public class CreateChatRoomCommandHandler(IChatRoomRepository chatRoomRepository) 
        : IRequestHandler<CreateChatRoomCommand, Guid>
    {
        private readonly IChatRoomRepository _chatRoomRepository = chatRoomRepository;

        public Task<Guid> Handle(CreateChatRoomCommand request, CancellationToken cancellationToken)
        {
            var chatRoom = new Domain.Aggregates.ChatRoom();

            var savedChatRoom = _chatRoomRepository.UpsertChatRoom(chatRoom);

            return Task.FromResult(savedChatRoom.Id);
        }
    }
}
