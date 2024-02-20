using System;
using System.ComponentModel;
using System.Globalization;

namespace Colosoft.Presentation.Input
{
    public class KeyGestureConverter : TypeConverter
    {
        private const char MODIFIERSDELIMITER = '+';
        internal const char DISPLAYSTRINGSEPARATOR = ',';

        private static readonly KeyConverter KeyConverter = new KeyConverter();
        private static readonly ModifierKeysConverter ModifierKeysConverter = new ModifierKeysConverter();

        internal static bool IsDefinedKey(Key key)
        {
            return key >= Key.None && key <= Key.OemClear;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string) && context != null && context.Instance != null)
            {
                var keyGesture = context.Instance as KeyGesture;
                if (keyGesture != null)
                {
                    return ModifierKeysConverter.IsDefinedModifierKeys(keyGesture.Modifiers)
                            && IsDefinedKey(keyGesture.Key);
                }
            }

            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string text)
            {
                string fullName = text.Trim();
                if (fullName == string.Empty)
                {
                    return new KeyGesture(Key.None);
                }

                string keyToken;
                string modifiersToken;
                string displayString;

                int index = fullName.IndexOf(DISPLAYSTRINGSEPARATOR);
                if (index >= 0)
                {
                    displayString = fullName.Substring(index + 1).Trim();
                    fullName = fullName.Substring(0, index).Trim();
                }
                else
                {
                    displayString = string.Empty;
                }

                index = fullName.LastIndexOf(MODIFIERSDELIMITER);
                if (index >= 0)
                {
                    modifiersToken = fullName.Substring(0, index);
                    keyToken = fullName.Substring(index + 1);
                }
                else
                {
                    modifiersToken = string.Empty;
                    keyToken = fullName;
                }

                ModifierKeys modifiers = ModifierKeys.None;
                var resultkey = KeyConverter.ConvertFrom(context, culture, keyToken);
                if (resultkey != null)
                {
                    var temp = ModifierKeysConverter.ConvertFrom(context, culture, modifiersToken);
                    if (temp != null)
                    {
                        modifiers = (ModifierKeys)temp;
                    }

                    return new KeyGesture((Key)resultkey, modifiers, displayString);
                }
            }

            throw this.GetConvertFromException(value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    var keyGesture = value as KeyGesture;
                    if (keyGesture != null)
                    {
                        if (keyGesture.Key == Key.None)
                        {
                            return string.Empty;
                        }

                        var strBinding = string.Empty;
                        var strKey = (string)KeyConverter.ConvertTo(context, culture, keyGesture.Key, destinationType) as string;
                        if (strKey != string.Empty)
                        {
                            strBinding += ModifierKeysConverter.ConvertTo(context, culture, keyGesture.Modifiers, destinationType) as string;
                            if (strBinding != string.Empty)
                            {
                                strBinding += MODIFIERSDELIMITER;
                            }

                            strBinding += strKey;

                            if (!string.IsNullOrEmpty(keyGesture.DisplayString) && keyGesture.DisplayString != strBinding)
                            {
                                strBinding += DISPLAYSTRINGSEPARATOR + keyGesture.DisplayString;
                            }
                        }

                        return strBinding;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }

            throw this.GetConvertToException(value, destinationType);
        }
    }
}
