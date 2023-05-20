using InterfaceTemplate = LemonDB.Interfaces.IDbEntityBuilder<LemonDB.Session>;
using ThisBuilder = LemonDB.Builders.SessionBuilder;

namespace LemonDB.Builders;

public class SessionBuilder : InterfaceTemplate
{
    Session InterfaceTemplate._alias { get; set; }
    Session Empty => new() { Date = DateTime.Now, Duration = 120, GameKey = "", Map = null, Participants = new List<Account>(), StartPlayersCount = 0, State = "CREATED" };
    public SessionBuilder()
    {
        ((InterfaceTemplate)this)._alias = Empty;
    }
    public SessionBuilder(Session template)
    {
        ((InterfaceTemplate)this)._alias = template;
    }

    public ThisBuilder SetMap(Map map)
    {
        ((InterfaceTemplate)this)._alias.Map = map;
        return this;
    }
    public ThisBuilder SetDate(DateTime date)
    {
        ((InterfaceTemplate)this)._alias.Date = date;
        return this;
    }
    public ThisBuilder SetDuration(double duration)
    {
        ((InterfaceTemplate)this)._alias.Duration = duration;
        return this;
    }
    public ThisBuilder SetStartPlayersCount(int startPlayersCount)
    {
        ((InterfaceTemplate)this)._alias.StartPlayersCount = startPlayersCount;
        return this;
    }
    public ThisBuilder SetState(string state)
    {
        ((InterfaceTemplate)this)._alias.State = state;
        return this;
    }
    public ThisBuilder SetKey(string key)
    {
        ((InterfaceTemplate)this)._alias.GameKey = key;
        return this;
    }
    public ThisBuilder SetParticipants(ICollection<Account> participants)
    {
        ((InterfaceTemplate)this)._alias.Participants = participants;
        ((InterfaceTemplate)this)._alias.StartPlayersCount = ((InterfaceTemplate)this)._alias.Participants.Count;
        return this;
    }
    public ThisBuilder RemoveParticipant(Account participant)
    {
        ((InterfaceTemplate)this)._alias.Participants.Remove(participant);
        ((InterfaceTemplate)this)._alias.StartPlayersCount = ((InterfaceTemplate)this)._alias.Participants.Count;
        return this;
    }
    public ThisBuilder AddParticipant(Account participants)
    {
        ((InterfaceTemplate)this)._alias.Participants.Add(participants);
        ((InterfaceTemplate)this)._alias.StartPlayersCount = ((InterfaceTemplate)this)._alias.Participants.Count;
        return this;
    }
    public Session Build()
    {
        var alias = ((InterfaceTemplate)this)._alias;
        ((InterfaceTemplate)this)._alias = Empty;
        return alias;
    }
}
