using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic04
{
    public class ProjectManager
    {
        private readonly EventEmitter _emitter;

        public ProjectManager(EventEmitter emitter)
        {
            _emitter = emitter;

            _subscribe();
        }

        private void _subscribe()
        {
            _emitter.On("work.done", "Project Manager", (Event eventData) =>
            {
                Console.WriteLine($"All work done at {eventData.ToString()}");

                _notifyClient();
            });

            _emitter.On("client.newRequirement", "Project Manager", (Event eventData) =>
            {
                Console.WriteLine("Received work from client");

                _emitter.Emit("work.received", eventData);
            });
        }

        public void _notifyClient()
        {
            RetryPolicy.Retry((int callTry) =>
            {
                if (callTry <= 2)
                {
                    Console.WriteLine("client don't respond");

                    throw new Exception("Client don't respond");
                }

                _emitter.Emit("call.client", new Event());
            });
        }
    }
}
