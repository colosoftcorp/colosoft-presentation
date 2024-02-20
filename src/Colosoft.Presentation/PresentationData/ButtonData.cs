using System;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation.PresentationData
{
    public class ButtonData : ControlData
    {
        public ButtonData()
        {
        }

        public ButtonData(Action executeMethod, Func<bool> canExecuteMethod = null)
            : base(executeMethod, canExecuteMethod)
        {
        }

        public ButtonData(Func<CancellationToken, Task> executeMethod, Func<bool> canExecuteMethod = null, bool useNewThread = false)
            : base(executeMethod, canExecuteMethod, useNewThread)
        {
        }

        public ButtonData(Action<object> executeMethod, Func<object, bool> canExecuteMethod = null)
            : base(executeMethod, canExecuteMethod)
        {
        }

        public ButtonData(Func<object, CancellationToken, Task> executeMethod, Func<object, bool> canExecuteMethod = null, bool useNewThread = false)
            : base(executeMethod, canExecuteMethod, useNewThread)
        {
        }
    }
}
