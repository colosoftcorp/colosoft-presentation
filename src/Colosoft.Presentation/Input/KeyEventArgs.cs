namespace Colosoft.Presentation.Input
{
    public class KeyEventArgs : InputEventArgs
    {
        public KeyEventArgs(
            InputDevice inputDevice,
            int timestamp,
            Key key,
            ModifierKeys modifiers)
            : base(inputDevice, timestamp)
        {
            this.Key = key;
            this.Modifiers = modifiers;
        }

        public Key Key { get; }

        public ModifierKeys Modifiers { get; }

        public Key RealKey => this.Key;
    }
}