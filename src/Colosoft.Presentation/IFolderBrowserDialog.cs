using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface IFolderBrowserDialog
    {
        string FileName { get; set; }

        bool DereferenceLinks { get; set; }

        bool ShowHiddenItems { get; set; }

        bool ShowPlacesList { get; set; }

        string InitialDirectory { get; set; }

        Task<bool?> ShowDialog(CancellationToken cancellationToken);
    }
}
