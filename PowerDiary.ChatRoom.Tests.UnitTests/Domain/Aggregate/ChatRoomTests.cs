using Xunit;

namespace PowerDiary.ChatRoom.Tests.UnitTests.Domain.Aggregate
{
    public class ChatRoomTests
    {
        [Fact]
        public void GivenChatRoomCreated_ThenAllItsDomainPropertiesShouldBeInstantiated()
        {
            // Given + When
            var chatRoom = new ChatRoom.Domain.Aggregates.ChatRoom();

            // Then
            Assert.Empty(chatRoom.Participants);
            Assert.Empty(chatRoom.CommentsPerParticipant);
            Assert.Empty(chatRoom.ReactionsPerParticipant);
        }

        [Fact]
        public void GivenChatRoom_WhenSameParticipantAttemptedToBeAddedTwice_ThenHeOrSheDoesNotGetAdded()
        {
            // Given
            var chatRoom = new ChatRoom.Domain.Aggregates.ChatRoom();
            var testParticipantName = "Test Participant";

            // When
            chatRoom.AddParticipant(testParticipantName);
            chatRoom.AddParticipant(testParticipantName);

            // Then
            var count = chatRoom.Participants.Count(p => p.Key.Name == testParticipantName);

            Assert.Equal(1, count);
        }

        [Fact]
        public void GivenChatRoomAndParticipantAdded_WhenParticipantRemoved_ThenHeOrSheShouldGetSoftDeleted()
        {
            // Given
            var chatRoom = new ChatRoom.Domain.Aggregates.ChatRoom();
            var testParticipantName = "Test Participant";
            chatRoom.AddParticipant(testParticipantName);

            // When 
            chatRoom.RemoveParticipant(testParticipantName);

            // Then
            var participant = chatRoom.Participants.First(p => p.Key.Name == testParticipantName);
            Assert.True(participant.Value is false);
        }

        [Fact]
        public void GivenChatRoomAndParticipantAdded_WhenAddCommentInvoked_ThenCommentIsAdded()
        {
            // Given
            var chatRoom = new ChatRoom.Domain.Aggregates.ChatRoom();
            var testParticipantName = "Test Participant";
            var testComment = "this is a testing comment";
            chatRoom.AddParticipant(testParticipantName);

            // When
            var at = DateTime.UtcNow;
            chatRoom.AddCommment(testParticipantName, testComment, at);

            // Then
            var comments = chatRoom.CommentsPerParticipant.First(cpr => cpr.Key.Name == testParticipantName).Value;
            Assert.Single(comments);
            Assert.True(comments.First().Content == testComment);
            Assert.True(comments.First().PostedAt == at);
        }

        [Fact]
        public void GivenChatRoomAndParticipantAddedAndRemoved_WhenAddCommentInvoked_ThenCommentIsNotAdded()
        {
            // Given
            var chatRoom = new ChatRoom.Domain.Aggregates.ChatRoom();
            var testParticipantName = "Test Participant";
            var testComment = "this is a testing comment";
            chatRoom.AddParticipant(testParticipantName);
            chatRoom.RemoveParticipant(testParticipantName);

            // When
            var at = DateTime.UtcNow;
            chatRoom.AddCommment(testParticipantName, testComment, at);

            // Then
            var comments = chatRoom.CommentsPerParticipant.First(cpr => cpr.Key.Name == testParticipantName).Value;
            Assert.Empty(comments);
        }

        [Fact]
        public void GivenChatRoomAndParticipantAdded_WhenAddReactionInvoked_ThenCommentIsAdded()
        {
            // Given
            var chatRoom = new ChatRoom.Domain.Aggregates.ChatRoom();
            var testParticipantName = "Test Participant";
            var otherTestParticipantName = "Other Test Participant";
            chatRoom.AddParticipant(testParticipantName);

            // When
            var at = DateTime.UtcNow;
            chatRoom.AddReaction(testParticipantName, otherTestParticipantName, ChatRoom.Domain.Enums.ReactionType.HighFive, at);

            // Then
            var reactions = chatRoom.ReactionsPerParticipant.First(cpr => cpr.Key.Name == testParticipantName).Value;
            Assert.Single(reactions);
            Assert.True(reactions.First().ReactionType == ChatRoom.Domain.Enums.ReactionType.HighFive);
            Assert.True(reactions.First().PostedAt == at);
        }

        [Fact]
        public void GivenChatRoomAndParticipantAddedAndRemoved_WhenAddReactionInvoked_ThenCommentIsNotAdded()
        {
            // Given
            var chatRoom = new ChatRoom.Domain.Aggregates.ChatRoom();
            var testParticipantName = "Test Participant";
            var otherTestParticipantName = "Other Test Participant";
            chatRoom.AddParticipant(testParticipantName);
            chatRoom.RemoveParticipant(testParticipantName);

            // When
            var at = DateTime.UtcNow;
            chatRoom.AddReaction(testParticipantName, otherTestParticipantName, ChatRoom.Domain.Enums.ReactionType.HighFive, at);

            // Then
            var reactions = chatRoom.ReactionsPerParticipant.First(cpr => cpr.Key.Name == testParticipantName).Value;
            Assert.Empty(reactions);
        }
    }
}
