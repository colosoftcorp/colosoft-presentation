namespace Colosoft.Presentation.PresentationData
{
    public class CheckBoxData : ControlData
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
