using System;
using System.Collections.Generic;

namespace Colosoft.Presentation
{
    public sealed class IconManager : IIconManager
    {
        private static IIconManager instance;
        private static object staticObjLock = new object();

        private readonly object objLock = new object();
        private Dictionary<string, IIcon> icons;

        public static IIconManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (staticObjLock)
                    {
                        if (instance == null)
                        {
                            instance = new IconManager();
                        }
                    }
                }

                return instance;
            }

            set
            {
                instance = value;
            }
        }

        private IconManager()
        {
        }

        private void Initialize()
        {
            if (this.icons != null)
            {
                return;
            }

            lock (this.objLock)
            {
                if (this.icons != null)
                {
                    return;
                }

                var assembly = typeof(IconManager).Assembly;

                var resources = new string[][]
                {
                    new string[] { "unknown", "Colosoft.Presentation.Resources.Icons.unknown_16x16.png", "16", "16" },
                };

                var icons1 = new Dictionary<string, IIcon>(StringComparer.InvariantCultureIgnoreCase);

                foreach (var i in resources)
                {
                    using (var stream = assembly.GetManifestResourceStream(i[1]))
                    {
                        var buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        icons1.Add(
                            i[0],
                            new Icon(
                                buffer,
                                int.Parse(i[2], System.Globalization.CultureInfo.InvariantCulture),
                                int.Parse(i[3], System.Globalization.CultureInfo.InvariantCulture)));
                    }
                }

                this.icons = icons1;
            }
        }

        public IIcon ExtractAssociatedIcon(string fileName)
        {
            this.Initialize();

            if (string.IsNullOrEmpty(fileName))
            {
                return this.icons["unknown"];
            }

            var extension = System.IO.Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(extension) || !this.icons.TryGetValue(extension, out var icon))
            {
                icon = this.icons["unknown"];
            }

            return icon;
        }

        private sealed class Icon : IIcon
        {
            private readonly byte[] iconBuffer;

            public int Width { get; }

            public int Height { get; }

            public void Save(System.IO.Stream outputStream)
            {
                if (outputStream is null)
                {
                    throw new ArgumentNullException(nameof(outputStream));
                }

                outputStream.Write(this.iconBuffer, 0, this.iconBuffer.Length);
            }

            public Icon(byte[] iconBuffer, int width, int height)
            {
                this.iconBuffer = iconBuffer;
                this.Width = width;
                this.Height = height;
            }
        }
    }
}
