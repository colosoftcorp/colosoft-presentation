using System;

namespace Colosoft.Presentation
{
    public static class DialogSettings
    {
        private static string popupRegion;

        public static event EventHandler PopupRegionChanged;

        public static string PopupRegion
        {
            get { return popupRegion; }
            set
            {
                if (popupRegion != value)
                {
                    popupRegion = value;
                    PopupRegionChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }
    }
}
