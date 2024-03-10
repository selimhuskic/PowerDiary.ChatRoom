using PowerDiary.ChatRoom.Application.Extensions;
using PowerDiary.ChatRoom.Domain.Enums;
using Xunit;

namespace PowerDiary.ChatRoom.Tests.UnitTests.Application.ExtensionTests
{
    public class EnumExtensionsTests
    {
        [Fact]
        public void GivenReactionTypeFriendlyOutputInvoked_UserFriendlyStringReturned()
        {
            // Given + When
            var reaction = ReactionType.HighFive;
            var responose = reaction.ReactionTypeFriendlyOutput();

            // Then
            Assert.Equal("high five", responose);
        }
    }
}
