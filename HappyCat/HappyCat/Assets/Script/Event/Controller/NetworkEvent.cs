using HC.Scene;

namespace HC.Event
{
    public class NetworkEvent
    {
        public static EventListeners ServiceEvents = new EventListeners();
    }

    public class GoogleLoginComplete : IEvent    {
        public bool success;
        public GoogleLoginComplete(bool success)
        {
            this.success = success;
        }
    }
}