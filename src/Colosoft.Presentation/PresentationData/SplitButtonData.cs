namespace Colosoft.Presentation.PresentationData
{
    public class SplitButtonData : MenuButtonData
    {
        private bool isChecked;
        private bool isCheckable;
        private ButtonData dropDownButtonData;

        public SplitButtonData()
            : this(false)
        {
        }

        public SplitButtonData(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }

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

        public bool IsCheckable
        {
            get
            {
                return this.isCheckable;
            }

            set
            {
                if (this.isCheckable != value)
                {
                    this.isCheckable = value;
                    this.OnPropertyChanged(nameof(this.IsCheckable));
                }
            }
        }

        public ButtonData DropDownButtonData
        {
            get
            {
                if (this.dropDownButtonData == null)
                {
                    this.dropDownButtonData = new ButtonData();
                }

                return this.dropDownButtonData;
            }
        }
    }
}
