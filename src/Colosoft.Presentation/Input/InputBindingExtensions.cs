using System.Collections.Generic;
using System.Linq;

namespace Colosoft.Presentation.Input
{
    public static class InputBindingExtensions
    {
        public static InputBinding FindMatch(this IEnumerable<InputBinding> bindings, object targetElement, InputEventArgs inputEventArgs)
        {
            foreach (var inputBinding in bindings.Reverse())
            {
                if ((inputBinding.Command != null) && (inputBinding.Gesture != null) &&
                    inputBinding.Gesture.Matches(targetElement, inputEventArgs))
                {
                    return inputBinding;
                }
            }

            return null;
        }
    }
}
