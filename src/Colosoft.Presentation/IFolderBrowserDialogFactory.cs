using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface IFolderBrowserDialogFactory
    {
        Task<IFolderBrowserDialog> Create(CancellationToken cancellationToken);
    }
}
