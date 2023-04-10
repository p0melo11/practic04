using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic04
{
    public class Client
    {
        private readonly EventEmitter _emitter;

        public Client(EventEmitter emitter)
        {
            _emitter = emitter;

            _subscribe();
        }

        public void GenerateWork()
        {
            for (int i = 0; i < 5; i++)
            {
                _emitter.Emit("client.newRequirement", new Event());
            }
        }
        private void _subscribe()
        {
            _emitter.On("call.client", "Client", (Event eventData) =>
            {
                Console.WriteLine("Client happy, all done");
            });
        }
    }
}
