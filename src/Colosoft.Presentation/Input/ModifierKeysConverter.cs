using System;
using System.ComponentModel;
using System.Globalization;

namespace Colosoft.Presentation.Input
{
    public class ModifierKeysConverter : TypeConverter
    {
        private const char ModifierDelimiter = '+';

        private static readonly ModifierKeys ModifierKeysFlag =
            ModifierKeys.Windows | ModifierKeys.Shift |
            ModifierKeys.Alt | ModifierKeys.Control;

        public static bool IsDefinedModifierKeys(ModifierKeys modifierKeys)
        {
            return modifierKeys == ModifierKeys.None || (((int)modifierKeys & ~(int)ModifierKeysFlag) == 0);
        }

        internal static string MatchModifiers(ModifierKeys modifierKeys)
        {
            string modifiers = string.Empty;
            switch (modifierKeys)
            {
                case ModifierKeys.Control: modifiers = "Ctrl"; break;
                case ModifierKeys.Shift: modifiers = "Shift"; break;
                case ModifierKeys.Alt: modifiers = "Alt"; break;
                case ModifierKeys.Windows: modifiers = "Windows"; break;
            }

            return modifiers;
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
            if (destinationType == typeof(string) && context != null && context.Instance != null &&
                    context.Instance is ModifierKeys)
            {
                return IsDefinedModifierKeys((ModifierKeys)context.Instance);
            }

            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string text)
            {
                string modifiersToken = text.Trim();
                ModifierKeys modifiers = this.GetModifierKeys(modifiersToken, CultureInfo.InvariantCulture);
                return modifiers;
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
                var modifiers = (ModifierKeys)value;

                if (!IsDefinedModifierKeys(modifiers))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)modifiers, typeof(ModifierKeys));
                }
                else
                {
                    string strModifiers = string.Empty;

                    if ((modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                    {
                        strModifiers += MatchModifiers(ModifierKeys.Control);
                    }

                    if ((modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
                    {
                        if (strModifiers.Length > 0)
                        {
                            strModifiers += ModifierDelimiter;
                        }

                        strModifiers += MatchModifiers(ModifierKeys.Alt);
                    }

                    if ((modifiers & ModifierKeys.Windows) == ModifierKeys.Windows)
                    {
                        if (strModifiers.Length > 0)
                        {
                            strModifiers += ModifierDelimiter;
                        }

                        strModifiers += MatchModifiers(ModifierKeys.Windows);
                    }

                    if ((modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                    {
                        if (strModifiers.Length > 0)
                        {
                            strModifiers += ModifierDelimiter;
                        }

                        strModifiers += MatchModifiers(ModifierKeys.Shift);
                    }

                    return strModifiers;
                }
            }

            throw this.GetConvertToException(value, destinationType);
        }

        private ModifierKeys GetModifierKeys(string modifiersToken, CultureInfo culture)
        {
            ModifierKeys modifiers = ModifierKeys.None;
            if (modifiersToken.Length != 0)
            {
                int offset = 0;
                do
                {
                    offset = modifiersToken.IndexOf(ModifierDelimiter);
                    string token = (offset < 0) ? modifiersToken : modifiersToken.Substring(0, offset);
                    token = token.Trim();
                    token = token.ToUpper(culture);

                    if (token == string.Empty)
                    {
                        break;
                    }

                    switch (token)
                    {
                        case "CONTROL":
                        case "CTRL":
                            modifiers |= ModifierKeys.Control;
                            break;

                        case "SHIFT":
                            modifiers |= ModifierKeys.Shift;
                            break;

                        case "ALT":
                            modifiers |= ModifierKeys.Alt;
                            break;

                        case "WINDOWS":
                        case "WIN":
                            modifiers |= ModifierKeys.Windows;
                            break;

                        default:
                            throw new NotSupportedException($"Unsupported_Modifier {token}");
                    }

                    modifiersToken = modifiersToken.Substring(offset + 1);
                }
                while (offset != -1);
            }

            return modifiers;
        }
    }
}
