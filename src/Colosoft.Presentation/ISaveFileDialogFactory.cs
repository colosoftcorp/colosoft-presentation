using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface ISaveFileDialogFactory
    {
        Task<ISaveFileDialog> Create(CancellationToken cancellationToken);
    }
}
