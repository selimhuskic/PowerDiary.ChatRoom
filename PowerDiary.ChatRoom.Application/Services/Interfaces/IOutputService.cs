using PowerDiary.ChatRoom.Application.Enums;
using PowerDiary.ChatRoom.Infrastructure.Models;

namespace PowerDiary.ChatRoom.Application.Services.Interfaces
{
    public interface IOutputService
    {
        List<string> Aggregate(IEnumerable<Event> events, Granularity granularity);
    }
}
