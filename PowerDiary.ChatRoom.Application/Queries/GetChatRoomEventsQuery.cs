using MediatR;
using PowerDiary.ChatRoom.Application.Enums;
using PowerDiary.ChatRoom.Application.Repositories.Interfaces;
using PowerDiary.ChatRoom.Application.Services.Interfaces;
using PowerDiary.ChatRoom.Application.DTOs;

namespace PowerDiary.ChatRoom.Application.Queries
{
    public record GetChatRoomEventsQuery(Guid ChatRoomId, int Granularity) : IRequest<ChatHistoryDto>;

    public class GetChatRoomEventsQueryHandler(
        IChatRoomRepository chatRoomRepository,
        IOutputService outputFormattingService) : IRequestHandler<GetChatRoomEventsQuery, ChatHistoryDto>
    {
        private readonly IChatRoomRepository _chatRoomRepository = chatRoomRepository;
        private readonly IOutputService _outputFormattingService = outputFormattingService;

        public Task<ChatHistoryDto> Handle(GetChatRoomEventsQuery request, CancellationToken cancellationToken)
        {
            var chatRoomEvents = _chatRoomRepository.GetChatRoomEvents(request.ChatRoomId) ?? 
                throw new ArgumentNullException($"Chat room with id {request.ChatRoomId} does not exist!");
            
            var formattedOutput = _outputFormattingService.Aggregate(chatRoomEvents, (Granularity)request.Granularity);

            return Task.FromResult(new ChatHistoryDto { Records = formattedOutput });
        }
    }
}
