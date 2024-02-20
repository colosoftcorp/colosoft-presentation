namespace Colosoft.Presentation.PresentationData
{
    public class TextBoxData : ControlData
    {
        private string text;

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.OnPropertyChanged(nameof(this.Text));
                }
            }
        }
    }
}
