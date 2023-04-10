using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic04
{
    public class EventEmitter
    {
        private Dictionary<string, Dictionary<string, Action<Event>>> _listeners = new Dictionary<string, Dictionary<string, Action<Event>>>();
        private Dictionary<string, Action<Event>> _debouncedEvents = new Dictionary<string, Action<Event>>();

        public bool Locked
        {
            get
            {
                return _debouncedEvents.Count != 0;
            }
        }

        public void On(string eventName, string listenerName, Action<Event> handler)
        {
            if (_listeners.ContainsKey(eventName))
            {
                _listeners[eventName][listenerName] = handler;

                return;
            }

            _listeners[eventName] = new Dictionary<string, Action<Event>>()
        {
            { listenerName, handler }
        };
        }

        public void Off(string eventName, string listenerName)
        {
            if (!_listeners.ContainsKey(eventName) || !_listeners[eventName].ContainsKey(listenerName))
            {
                return;
            }

            _listeners[eventName].Remove(listenerName);
        }

        public void Emit(string eventName, Event eventData)
        {
            if (!_listeners.ContainsKey(eventName))
            {
                return;
            }

            foreach (var entry in _listeners[eventName])
            {
                entry.Value(eventData);
            }
        }

        public void DebounceEmit(string eventName, Event eventData, int delay)
        {
            if (_debouncedEvents.ContainsKey(eventName))
            {
                _debouncedEvents[eventName](eventData);

                return;
            }

            _debouncedEvents[eventName] = DebouncePolicy.Debounce((Event eventData) =>
            {
                Emit(eventName, eventData);

                _debouncedEvents.Remove(eventName);
            }, delay);

            _debouncedEvents[eventName](eventData);
        }
    }
}
