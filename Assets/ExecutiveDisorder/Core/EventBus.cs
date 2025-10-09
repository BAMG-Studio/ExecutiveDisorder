namespace ExecutiveDisorder.Core
{
    using System;
    using System.Collections.Generic;

    public class EventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subs = new();

        public void Subscribe<T>(Action<T> cb)
        {
            if (cb == null) return;
            var t = typeof(T);
            if (!_subs.TryGetValue(t, out var list))
            {
                list = new List<Delegate>(4);
                _subs[t] = list;
            }
            if (!list.Contains(cb)) list.Add(cb);
        }

        public void Unsubscribe<T>(Action<T> cb)
        {
            if (cb == null) return;
            var t = typeof(T);
            if (_subs.TryGetValue(t, out var list))
            {
                list.Remove(cb);
            }
        }

        public void Publish<T>(T evt)
        {
            var t = typeof(T);
            if (_subs.TryGetValue(t, out var list))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] is Action<T> a) a(evt);
                }
            }
        }
    }
}

