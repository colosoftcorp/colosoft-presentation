using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface ISaveFileDialog : IFileDialog
    {
        bool CreatePrompt { get; set; }

        bool OverwritePrompt { get; set; }

        Task<System.IO.Stream> OpenFile(CancellationToken cancellationToken);
    }
}
