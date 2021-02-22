public class EventLog
{
    private string actor;
    private Verb verb;
    private string action;

    public EventLog(string actor, Verb verb, string action)
    {
        this.actor = actor;
        this.verb = verb;
        this.action = action;
    }

    public override string ToString()
    {
        return string.Format("{0};{1};{2}", this.actor, this.verb.ToString(), this.action);
    }
}
