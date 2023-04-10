using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic04
{
    public class RetryPolicy
    {
        public static Func<int, int> ExpDelay = (int tryNo) => (int)Math.Pow(Math.E, tryNo);

        public static async Task Retry(Action<int> handler, int times, Func<int, int> delay)
        {
            Exception? exception = null;

            for (int i = 0; i < times; i++)
            {
                try
                {
                    handler(i + 1);
                    exception = null;

                    break;
                }
                catch (Exception e)
                {
                    exception = e;
                }

                await Task.Delay(delay(i + 1));
            }

            if (exception != null)
            {
                throw exception;
            }
        }

        public static async Task Retry(Action<int> handler, int times)
        {
            await Retry(handler, times, ExpDelay);
        }


        public static async Task Retry(Action<int> handler, Func<int, int> delay)
        {
            await Retry(handler, 3, delay);
        }

        public static async Task Retry(Action<int> handler)
        {
            await Retry(handler, 3, ExpDelay);
        }
    }
}
