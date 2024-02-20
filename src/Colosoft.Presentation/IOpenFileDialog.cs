using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface IOpenFileDialog : IFileDialog
    {
        bool Multiselect { get; set; }

        Task<System.IO.Stream> OpenFile(CancellationToken cancellationToken);

        Task<System.IO.Stream[]> OpenFiles(CancellationToken cancellationToken);

        IEnumerable<Func<CancellationToken, Task<System.IO.Stream>>> OpenFiles();
    }
}
