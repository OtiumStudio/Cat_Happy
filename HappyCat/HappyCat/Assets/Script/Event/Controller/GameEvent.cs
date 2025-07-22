using HC.Scene;

namespace HC.Event
{
    public class GameEvent
    {
        public static EventListeners ServiceEvents = new EventListeners();
    }

    public class JoinCatEvent : IEvent
    {
        public Cat_Actor Cat { get; protected set; }

        public JoinCatEvent(Cat_Actor cat)
        {
            this.Cat = cat;
        }
    }
}