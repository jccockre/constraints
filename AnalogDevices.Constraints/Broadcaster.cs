
using System.Collections.Generic;

namespace AnalogDevices.Constraints
{
    public abstract class Broadcaster : ISubscriber
    {
        private readonly List<ISubscriber> subscribers = new List<ISubscriber>();

        public void AddSubscriber(ISubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            if (subscribers.Contains(subscriber))
            {
                subscribers.Remove(subscriber);
            }
        }

        public virtual void NotifyChanged()
        {
            foreach (ISubscriber subscriber in subscribers)
            {
                subscriber.NotifyChanged();
            }
        }
    }
}
