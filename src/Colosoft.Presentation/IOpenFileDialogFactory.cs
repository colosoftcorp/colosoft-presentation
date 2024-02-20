using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface IOpenFileDialogFactory
    {
        Task<IOpenFileDialog> Create(CancellationToken cancellationToken);
    }
}
