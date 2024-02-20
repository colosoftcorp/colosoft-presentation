using System;

namespace Colosoft.Presentation.Input
{
    public class CommandContext : IDisposable
    {
        private bool disposed;

        public CommandContext()
        {
        }

        public CommandContext(object bindingSource, object commandSource, object commandParameter)
        {
            this.CommandSource = commandSource;
            this.BindingSource = bindingSource;
            this.CommandParameter = commandParameter;
        }

        ~CommandContext()
        {
            this.Dispose(false);
        }

        public object BindingSource { get; set; }

        public object CommandSource { get; set; }

        public object CommandParameter { get; set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposed = true;
            }
        }
    }
}
