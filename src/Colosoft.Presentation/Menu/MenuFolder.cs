using System;

namespace Colosoft.Presentation.Menu
{
    internal class MenuFolder : MenuItem, IMenuFolder
    {
        public MenuFolder(Uri path)
            : base(path)
        {
        }
    }
}
