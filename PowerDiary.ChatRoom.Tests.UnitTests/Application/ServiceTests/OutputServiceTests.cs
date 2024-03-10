using PowerDiary.ChatRoom.Application.Models;
using PowerDiary.ChatRoom.Application.Services;
using Xunit;

namespace PowerDiary.ChatRoom.Tests.UnitTests.Application.ServiceTests
{
    public class OutputServiceTests
    {
        private readonly OutputService _outputService;

        public OutputServiceTests()
        {
            _outputService = new OutputService();
        }

        [Fact]
        public void GivenTwoEvents_WhenGranularityIsMinuteByMinute_ThenTwoRecordsReturned()
        {
            // Given
            var at = DateTime.UtcNow;

            var events = new List<Event>
            {
                new(EventType.EnterTheRoom, "enter", at),
                new(EventType.Comment, "comment", at),
            };

            // When
            var response = _outputService.Aggregate(events, ChatRoom.Application.Enums.Granularity.Minute);

            // Then
            Assert.Equal(events.Count, response.Count);
        }

        [Fact]
        public void GivenThreeEvents_WhenGranularityIsMinuteByMinute_ThenThreeRecordsReturned()
        {
            // Given
            var at = DateTime.UtcNow;

            var events = new List<Event>
            {
                new(EventType.EnterTheRoom, "enter", at),
                new(EventType.Comment, "comment", at),
                new(EventType.LeaveTheRoom, "leave", at)
            };

            // When
            var response = _outputService.Aggregate(events, ChatRoom.Application.Enums.Granularity.Minute);

            // Then
            Assert.Equal(events.Count, response.Count);
        }
    }
}
