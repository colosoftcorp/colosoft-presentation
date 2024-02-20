using System;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public class NotifyBusyOptions
    {
        public string Message { get; set; }

        public string CancelText { get; set; }

        public Func<CancellationToken, Task> CancelCallback { get; set; }
    }
}