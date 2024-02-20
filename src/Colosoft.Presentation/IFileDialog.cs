using System;
using System.Threading;
using System.Threading.Tasks;

namespace Colosoft.Presentation
{
    public interface IFileDialog : ITitleContainerView
    {
        event System.ComponentModel.CancelEventHandler FileOk;

        bool AddExtension { get; set; }

        bool CheckFileExists { get; set; }

        bool CheckPathExists { get; set; }

        string DefaultExt { get; set; }

        bool DereferenceLinks { get; set; }

        string FileName { get; set; }

#pragma warning disable CA1819 // Properties should not return arrays
        string[] FileNames { get; }
#pragma warning restore CA1819 // Properties should not return arrays

        string Filter { get; set; }

        int FilterIndex { get; set; }

        string InitialDirectory { get; set; }

        bool ValidateNames { get; set; }

        void Reset();

        Task<bool?> ShowDialog(CancellationToken cancellationToken);
    }
}
