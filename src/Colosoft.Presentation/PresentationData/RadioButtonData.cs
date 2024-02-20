namespace Colosoft.Presentation.PresentationData
{
    public class RadioButtonData : ControlData
    {
        private bool isChecked;

        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }

            set
            {
                if (this.isChecked != value)
                {
                    this.isChecked = value;
                    this.OnPropertyChanged(nameof(this.IsChecked));
                }
            }
        }
    }
}
