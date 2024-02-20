using System;

namespace Colosoft.Presentation.Input
{
    public class CommandDescription
    {
        private InputGestureCollection gestures;

        public IMessageFormattable Text { get; set; }

        public IMessageFormattable Description { get; set; }

        public InputGestureCollection Gestures => this.gestures;

        public string GesturesString { get; }

        public IMessageFormattable ToolTip { get; set; }

        public CommandDescription(IMessageFormattable text)
            : this(text, string.Empty.GetFormatter(), string.Empty, string.Empty.GetFormatter(), string.Empty)
        {
        }

        public CommandDescription(IMessageFormattable text, IMessageFormattable description)
            : this(text, description, string.Empty, string.Empty.GetFormatter(), string.Empty)
        {
        }

        public CommandDescription(IMessageFormattable text, IMessageFormattable description, string gestures)
            : this(text, description, gestures, string.Empty.GetFormatter(), string.Empty)
        {
        }

        public CommandDescription(IMessageFormattable text, IMessageFormattable description, string gestures, IMessageFormattable toolTip)
            : this(text, description, gestures, toolTip, string.Empty)
        {
        }

        public CommandDescription(IMessageFormattable text, IMessageFormattable description, string gestures, IMessageFormattable toolTip, string gesturesText)
        {
            this.Text = text;
            this.Description = description;
            this.ToolTip = toolTip;
            this.GesturesString = gestures;

            this.SetupGestures(gestures, gesturesText);
        }

        private void SetupGestures(string keyGestures, string displayStrings)
        {
            if (string.IsNullOrEmpty(displayStrings))
            {
                displayStrings = keyGestures;
            }

            while (!string.IsNullOrEmpty(keyGestures))
            {
                string currentDisplay;
                string currentGesture;
                int index = keyGestures.IndexOf(";", StringComparison.Ordinal);
                if (index >= 0)
                {
                    currentGesture = keyGestures.Substring(0, index);
                    keyGestures = keyGestures.Substring(index + 1);
                }
                else
                {
                    currentGesture = keyGestures;
                    keyGestures = string.Empty;
                }

                index = displayStrings.IndexOf(";", StringComparison.Ordinal);
                if (index >= 0)
                {
                    currentDisplay = displayStrings.Substring(0, index);
                    displayStrings = displayStrings.Substring(index + 1);
                }
                else
                {
                    currentDisplay = displayStrings;
                    displayStrings = string.Empty;
                }

                this.CreateFromResourceStrings(currentGesture, currentDisplay);
            }
        }

        private void CreateFromResourceStrings(string keyGestureToken, string keyDisplayString)
        {
            if (this.gestures == null)
            {
                this.gestures = new InputGestureCollection();
            }

            KeyGesture.AddGesturesFromResourceStrings(keyGestureToken, keyDisplayString, this.gestures);
        }
    }
}
