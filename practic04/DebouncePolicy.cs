using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic04
{
    public static class DebouncePolicy
    {
        public static Action<T> Debounce<T>(this Action<T> callback, int delay)
        {
            CancellationTokenSource? cancelTokenSource = null;

            return arg =>
            {
                cancelTokenSource?.Cancel();
                cancelTokenSource = new CancellationTokenSource();

                Task.Delay(delay, cancelTokenSource.Token)
                    .ContinueWith(t =>
                    {
                        if (t.IsCompletedSuccessfully)
                        {
                            callback(arg);
                        }
                    }, TaskScheduler.Default);
            };
        }
    }
}
