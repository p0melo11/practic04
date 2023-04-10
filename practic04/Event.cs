using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic04
{
    public class Event
    {
        public readonly DateTime EmitMoment;

        public Event()
        {
            EmitMoment = DateTime.Now;
        }
    }

}
