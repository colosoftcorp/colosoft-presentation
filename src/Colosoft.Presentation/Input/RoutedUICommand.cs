using System;

namespace Colosoft.Presentation.Input
{
    public abstract class RoutedUICommand : RoutedCommand
    {
        private string text;

        protected RoutedUICommand()
            : base()
        {
            this.text = string.Empty;
        }

        protected RoutedUICommand(string text, string name, Type ownerType)
            : this(text, name, ownerType, null)
        {
        }

        protected RoutedUICommand(string text, string name, Type ownerType, InputGestureCollection inputGestures)
            : base(name, ownerType, inputGestures)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            this.text = text;
        }

        public string Text
        {
            get
            {
                return this.text ?? string.Empty;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.text = value;
            }
        }
    }
}
