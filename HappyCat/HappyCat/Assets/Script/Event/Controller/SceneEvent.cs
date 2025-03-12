using HC.Scene;

namespace HC.Event
{
    public class SceneEvent
    {
        public static EventListeners ServiceEvents = new EventListeners();
    }

    public class SceneLoadStartEvent : IEvent
    {
        public SceneKind Kind { get; protected set; }

        public SceneLoadStartEvent(SceneKind kind)
        {
            this.Kind = kind;
        }
    }

    public class SceneLoadProgressEvent : IEvent
    {
        public float Progress { get; protected set; }

        public SceneLoadProgressEvent(float progress)
        {
            this.Progress = progress;
        }
    }

    public class SceneLoadEndEvent : IEvent
    {
        public SceneLoadEndEvent()
        {
        }
    }
}