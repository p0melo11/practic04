using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic04
{
    public class Developer
    {
        private readonly EventEmitter _emitter;

        public Developer(EventEmitter emitter)
        {
            _emitter = emitter;

            _subscribe();
        }

        private void _subscribe()
        {
            _emitter.On("work.received", "Developer", (Event eventData) =>
            {
                Console.WriteLine("dev received task");

                _doProject();
            });
        }

        private void _doProject()
        {
            _emitter.DebounceEmit("work.done", new Event(), 1000);
        }
    }
}
