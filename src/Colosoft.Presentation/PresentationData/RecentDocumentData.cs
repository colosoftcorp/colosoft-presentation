namespace Colosoft.Presentation.PresentationData
{
    public class RecentDocumentData : ApplicationMenuItemData
    {
        private int index;

        public int Index
        {
            get
            {
                return this.index;
            }

            set
            {
                if (this.index != value)
                {
                    this.index = value;
                    this.OnPropertyChanged(nameof(this.Index));
                }
            }
        }
    }
}
