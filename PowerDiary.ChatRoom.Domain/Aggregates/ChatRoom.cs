using PowerDiary.ChatRoom.Domain.Models;

namespace PowerDiary.ChatRoom.Domain.Aggregates
{
    public class ChatRoom
    {
        public Guid Id { get; }
        public Dictionary<Participant, bool> Participants { get; }
        public Dictionary<Participant, List<Comment>> CommentsPerParticipant { get; }
        public Dictionary<Participant, List<Reaction>> ReactionsPerParticipant { get; set; }

        public ChatRoom()
        {
            Id = Guid.NewGuid();
            CommentsPerParticipant = [];
            Participants = [];
            ReactionsPerParticipant = [];
        }

        public void AddCommment(string participantName, Comment comment)
        {
            var isParticipantInAndActive = Participants.Any(p => p.Key.Name == participantName && p.Value);

            if (!isParticipantInAndActive)
                return;

            var participant = Participants.First(p => p.Key.Name == participantName).Key;

            CommentsPerParticipant[participant].Add(comment);
        }

        public void AddReaction(string participantName, Reaction reaction)
        {
            var isParticipantInAndActive = Participants.Any(p => p.Key.Name == participantName && p.Value);

            if (!isParticipantInAndActive)
                return;

            var participant = Participants.First(p => p.Key.Name == participantName).Key;

            ReactionsPerParticipant[participant].Add(reaction);
        }

        public void AddParticipant(string participantName)
        {
            if (Participants.Any(p => p.Key.Name == participantName))
                return;

            var participant = new Participant(participantName);

            Participants.Add(participant, true);
            CommentsPerParticipant.Add(participant, []);
            ReactionsPerParticipant.Add(participant, []);
        }

        public void RemoveParticipant(string participantName)
        {
            var participant = Participants.First(p => p.Key.Name == participantName).Key;
            Participants[participant] = false;
        }            
    }
}
