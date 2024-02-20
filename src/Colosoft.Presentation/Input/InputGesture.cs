namespace Colosoft.Presentation.Input
{
    public abstract class InputGesture
    {
        public abstract bool Matches(object targetElement, InputEventArgs inputEventArgs);
    }
}
