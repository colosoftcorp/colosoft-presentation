using System;
using System.ComponentModel;
using System.Globalization;

namespace Colosoft.Presentation.Input
{
    [TypeConverter(typeof(KeyGestureConverter))]
    public class KeyGesture : InputGesture
    {
        private const string MULTIPLEGESTUREDELIMITER = ";";

        private static readonly TypeConverter KeyGestureConverter = new KeyGestureConverter();

        private readonly ModifierKeys modifiers = ModifierKeys.None;
        private readonly Key key = Key.None;
        private readonly string displayString;

        public KeyGesture(Key key)
            : this(key, ModifierKeys.None)
        {
        }

        public KeyGesture(Key key, ModifierKeys modifiers)
            : this(key, modifiers, displayString: string.Empty, true)
        {
        }

        public KeyGesture(Key key, ModifierKeys modifiers, string displayString)
            : this(key, modifiers, displayString, true)
        {
        }

        internal KeyGesture(Key key, ModifierKeys modifiers, bool validateGesture)
            : this(key, modifiers, string.Empty, validateGesture)
        {
        }

        private KeyGesture(Key key, ModifierKeys modifiers, string displayString, bool validateGesture)
        {
            if (!ModifierKeysConverter.IsDefinedModifierKeys(modifiers))
            {
                throw new InvalidEnumArgumentException(nameof(modifiers), (int)modifiers, typeof(ModifierKeys));
            }

            if (!IsDefinedKey(key))
            {
                throw new InvalidEnumArgumentException(nameof(key), (int)key, typeof(Key));
            }

            if (validateGesture && !IsValid(key, modifiers))
            {
                throw new NotSupportedException($"KeyGesture_Invalid {modifiers} {key}");
            }

            this.modifiers = modifiers;
            this.key = key;
            this.displayString = displayString ?? throw new ArgumentNullException(nameof(displayString));
        }

        public ModifierKeys Modifiers => this.modifiers;

        public Key Key => this.key;

        public string DisplayString => this.displayString;

        internal static bool IsDefinedKey(Key key)
        {
            return key >= Key.None && key <= Key.OemClear;
        }

        internal static bool IsValid(Key key, ModifierKeys modifiers)
        {
            if (!((key >= Key.F1 && key <= Key.F24) || (key >= Key.NumPad0 && key <= Key.Divide)))
            {
                if ((modifiers & (ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Windows)) != 0)
                {
                    switch (key)
                    {
                        case Key.LeftCtrl:
                        case Key.RightCtrl:
                        case Key.LeftAlt:
                        case Key.RightAlt:
                        case Key.LWin:
                        case Key.RWin:
                            return false;

                        default:
                            return true;
                    }
                }
                else if ((key >= Key.D0 && key <= Key.D9) || (key >= Key.A && key <= Key.Z))
                {
                    return false;
                }
            }

            return true;
        }

        internal static void AddGesturesFromResourceStrings(string keyGestures, string displayStrings, InputGestureCollection gestures)
        {
            while (!string.IsNullOrEmpty(keyGestures))
            {
                string keyGestureToken;
                string keyDisplayString;

                int index = keyGestures.IndexOf(MULTIPLEGESTUREDELIMITER, StringComparison.Ordinal);
                if (index >= 0)
                {
                    keyGestureToken = keyGestures.Substring(0, index);
                    keyGestures = keyGestures.Substring(index + 1);
                }
                else
                {
                    keyGestureToken = keyGestures;
                    keyGestures = string.Empty;
                }

                index = displayStrings.IndexOf(MULTIPLEGESTUREDELIMITER, StringComparison.Ordinal);
                if (index >= 0)
                {
                    keyDisplayString = displayStrings.Substring(0, index);
                    displayStrings = displayStrings.Substring(index + 1);
                }
                else
                {
                    keyDisplayString = displayStrings;
                    displayStrings = string.Empty;
                }

                KeyGesture keyGesture = CreateFromResourceStrings(keyGestureToken, keyDisplayString);

                if (keyGesture != null)
                {
                    gestures.Add(keyGesture);
                }
            }
        }

        internal static KeyGesture CreateFromResourceStrings(string keyGestureToken, string keyDisplayString)
        {
            if (!string.IsNullOrEmpty(keyDisplayString))
            {
                keyGestureToken += Input.KeyGestureConverter.DISPLAYSTRINGSEPARATOR + keyDisplayString;
            }

            return KeyGestureConverter.ConvertFromInvariantString(keyGestureToken) as KeyGesture;
        }

        public string GetDisplayStringForCulture(CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(this.displayString))
            {
                return this.displayString;
            }

            return (string)KeyGestureConverter.ConvertTo(null, culture, this, typeof(string));
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            var keyEventArgs = inputEventArgs as KeyEventArgs;
            if (keyEventArgs != null && IsDefinedKey(keyEventArgs.Key))
            {
                return ((int)this.Key == (int)keyEventArgs.RealKey) && (this.Modifiers == keyEventArgs.Modifiers);
            }

            return false;
        }
    }
}
