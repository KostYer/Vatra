using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore
{

    public interface ISignalBus
    {
        void Emit<T>(Action<T> subscribers) where T : class, IEventBusSubscriber;
        void Subscribe<S>(S subscriber, int priority = 0) where S : IEventBusSubscriber;
        void UnSubscribe<S>(S suscriber) where S : IEventBusSubscriber;
    }
    public class SignalBus : ISignalBus
    {
         private Dictionary<RuntimeTypeHandle, List<PrioritySubscriber>> SubscribersByType = new Dictionary<RuntimeTypeHandle, List<PrioritySubscriber>>();

        public void Emit<T>(Action<T> action) where T : class, IEventBusSubscriber
        {
            Type type = typeof(T);
            RuntimeTypeHandle handle = type.TypeHandle;
            if (SubscribersByType.ContainsKey(handle))
            {

                List<PrioritySubscriber> eventsSubs = SubscribersByType[handle];
                List<PrioritySubscriber> copySubs = new List<PrioritySubscriber>();
                copySubs.AddRange(eventsSubs);
                foreach (PrioritySubscriber prioritySub in copySubs)
                {
                    action.Invoke(prioritySub.Subscriber as T);
                }
                
            }
        }

        public int SubsNumber()
        {
            return SubscribersByType.Count;
        }

        public void Subscribe<S>(S subscriber, int prioity = 0) where S : IEventBusSubscriber
        {
            Type type = typeof(S);
            RuntimeTypeHandle handle = type.TypeHandle;
            if (SubscribersByType.ContainsKey(handle))
            {
                //if (SubscribersByType[handle].Contains(subscriber))
                if (SubscribersByType[handle].Any(s => s.Subscriber.GetHashCode() == subscriber.GetHashCode()))
                {
                    Debug.LogError($"Object of type->{type.Name} try to subscribe twice");
                    return;
                }
                SubscribersByType[handle].Add(new PrioritySubscriber
                {
                    Subscriber = subscriber,
                    Priority = prioity
                });
                SubscribersByType[handle] = SubscribersByType[handle].OrderBy(s => s.Priority).ToList();

                //Debug.Log(subscriber.ToString());
            }
            else
            {
                SubscribersByType.Add(handle, new List<PrioritySubscriber>
                {
                    new PrioritySubscriber
                    {
                        Subscriber = subscriber,
                        Priority = prioity
                    }
                });

                //Debug.Log(subscriber.ToString());
            }
        }

        public void UnSubscribe<S>(S subscriber) where S : IEventBusSubscriber
        {
            Type type = typeof(S);
            RuntimeTypeHandle handle = type.TypeHandle;
            if (SubscribersByType.ContainsKey(handle))
            {
                var sub = SubscribersByType[handle].FirstOrDefault(s => s.Subscriber.GetHashCode() == subscriber.GetHashCode());
                if (sub != null)
                {
                    SubscribersByType[handle].Remove(sub);
                }
            }
        }

        private class PrioritySubscriber
        {
            public IEventBusSubscriber Subscriber;
            public int Priority;
        }
    }
    
}
