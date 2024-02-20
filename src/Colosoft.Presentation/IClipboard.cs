using System.Threading;
using System.Threading.Tasks;

namespace Colosoft
{
    public interface IClipboard
    {
        Task Flush(CancellationToken cancellationToken);

        Task<bool> ContainsData(string format, CancellationToken cancellationToken);

        Task SetImage(Media.Drawing.IImage image, CancellationToken cancellationToken);

        Task SetText(string text, CancellationToken cancellationToken);

        Task SetText(string text, Text.TextDataFormat format, CancellationToken cancellationToken);

        Task<object> GetData(string format, CancellationToken cancellationToken);

        Task<IClipboardDataObject> GetDataObject(CancellationToken cancellationToken);

        Task SetData(string format, object data, CancellationToken cancellationToken);

        Task SetDataObject(object data, CancellationToken cancellationToken);

        Task SetDataObject(object data, bool copy, CancellationToken cancellationToken);
    }
}
