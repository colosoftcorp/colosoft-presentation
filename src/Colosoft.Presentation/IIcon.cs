using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colosoft.Presentation
{
    public interface IIcon
    {
        int Width { get; }

        int Height { get; }

        void Save(System.IO.Stream outputStream);
    }
}
