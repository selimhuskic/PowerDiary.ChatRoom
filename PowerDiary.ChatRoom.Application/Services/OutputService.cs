using PowerDiary.ChatRoom.Application.Enums;
using PowerDiary.ChatRoom.Application.Services.Interfaces;
using PowerDiary.ChatRoom.Application.Models;

namespace PowerDiary.ChatRoom.Application.Services
{
    public class OutputService() : IOutputService
    {
        public List<string> Aggregate(IEnumerable<Event> events, Granularity granularity)
        {
            return granularity switch
            {
                Granularity.Minute => AggregateEventsPerMinute(events),
                Granularity.Hour => AggregateEventsPerHour(events),
                _ => AggregateEventsPerMinute(events),
            };
        }

        private static List<string> AggregateEventsPerMinute(IEnumerable<Event> events)
            => events.Select(e => e.Content).ToList();

        private static List<string> AggregateEventsPerHour(IEnumerable<Event> events)
        {
            var response = new List<string>();

            var eventsByHour = events
                .GroupBy(
                    e => e.At.Hour,
                    e => e.ChatRoomEvent,
                    (key, cre) => new { Hour = key, Events = cre.ToList() });

            FormatOutputForHourlyAggregation();

            return response;

            void FormatOutputForHourlyAggregation()
            {
                foreach (var eventsInHour in eventsByHour)
                {
                    response.Add(Environment.NewLine + $"{eventsInHour.Hour}:00");

                    var enterEvents = eventsInHour.Events.Where(e => e == EventType.EnterTheRoom);

                    if (enterEvents.Any())
                        response.Add($"{enterEvents.Count()} entered");

                    var leftEvents = eventsInHour.Events.Where(e => e == EventType.LeaveTheRoom);

                    if (leftEvents.Any())
                        response.Add($"{leftEvents.Count()} left");

                    var highFiveEvents = eventsInHour.Events.Where(e => e == EventType.LeaveTheRoom);

                    if (highFiveEvents.Any())
                        response.Add($"{highFiveEvents.Count()} high fived");

                    var commentEvents = eventsInHour.Events.Where(e => e == EventType.Comment);

                    if (commentEvents.Any())
                        response.Add($"{commentEvents.Count()} commented");
                }

            }
        }
    }
}
