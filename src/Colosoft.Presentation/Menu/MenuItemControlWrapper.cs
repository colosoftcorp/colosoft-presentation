using System;
using System.ComponentModel;
using System.Linq;

namespace Colosoft.Presentation.Menu
{
    public class MenuItemControlWrapper : IMenuItem, IConfigurableCommandData, IGestureCommandData
    {
        private readonly string name;
        private readonly PresentationData.ControlData controlData;
        private Uri path;
        private IMenuPosition position;

        public event PropertyChangedEventHandler PropertyChanged;

        public MenuItemControlWrapper(Uri path, PresentationData.ControlData controlData)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (controlData == null)
            {
                throw new ArgumentNullException(nameof(controlData));
            }

            if (path.Scheme != MenuItem.MenuScheme)
            {
                throw new InvalidOperationException(
                    ResourceMessageFormatter.Create(() => Properties.Resources.MenuItem_InvalidPathScheme).Format());
            }

            this.position = new AbsolutePosition(-1);
            this.path = path;
            this.name = path.Segments.LastOrDefault(f => f != "/" && f != "\\") ?? path.Host;
            this.controlData = controlData;
            this.controlData.PropertyChanged += this.ControlDataPropertyChanged;
        }

        public MenuItemControlWrapper(string name, int index, PresentationData.ControlData controlData)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (controlData == null)
            {
                throw new ArgumentNullException(nameof(controlData));
            }

            this.position = new AbsolutePosition(index);
            this.name = name;
            this.path = new Uri($"{MenuItem.MenuScheme}://{name}");
            this.controlData = controlData;
            this.controlData.PropertyChanged += this.ControlDataPropertyChanged;
        }

        public MenuItemControlWrapper(string name, PresentationData.ControlData controlData)
            : this(name, -1, controlData)
        {
        }

        public string Name => this.name;

        public IMessageFormattable Label
        {
            get { return this.controlData.Label; }
            set { this.controlData.Label = value; }
        }

        public Uri LargeImage
        {
            get { return this.controlData.LargeImage; }
            set { this.controlData.LargeImage = value; }
        }

        public Uri SmallImage
        {
            get { return this.controlData.SmallImage; }
            set { this.controlData.SmallImage = value; }
        }

        public IMessageFormattable ToolTipTitle
        {
            get { return this.controlData.ToolTipTitle; }
            set { this.controlData.ToolTipTitle = value; }
        }

        public IMessageFormattable ToolTipDescription
        {
            get { return this.controlData.ToolTipDescription; }
            set { this.controlData.ToolTipDescription = value; }
        }

        public string Gestures
        {
            get { return this.controlData.Gestures; }
            set { this.controlData.Gestures = value; }
        }

        public IMenuPosition Position
        {
            get { return this.position; }
            set
            {
                if (this.position != value)
                {
                    this.position = value;
                    this.OnPropertyChanged(nameof(this.Position));
                }
            }
        }

        public Uri Path
        {
            get { return this.path; }
            set
            {
                if (this.path != value)
                {
                    this.path = value;
                    this.OnPropertyChanged(nameof(this.Path));
                }
            }
        }

        public System.Windows.Input.ICommand Command
        {
            get { return this.controlData.Command; }
            set { this.controlData.Command = value; }
        }

        public Input.IRoutedCommand RoutedCommand
        {
            get { return this.controlData.RoutedCommand; }
            set { this.controlData.RoutedCommand = value; }
        }

        public object CommandParameter
        {
            get => this.controlData is ICommandParameterContainer commandParameterContainer
                ? commandParameterContainer.CommandParameter
                : null;
            set
            {
                if (this.controlData is ICommandParameterContainer commandParameterContainer)
                {
                    commandParameterContainer.CommandParameter = value;
                }
            }
        }

        protected void OnPropertyChanged(params string[] names)
        {
            if (this.PropertyChanged != null)
            {
                foreach (var i in names)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(i));
                }
            }
        }

        private void ControlDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PresentationData.ControlData.Label):
                case nameof(PresentationData.ControlData.LargeImage):
                case nameof(PresentationData.ControlData.SmallImage):
                case nameof(PresentationData.ControlData.ToolTipTitle):
                case nameof(PresentationData.ControlData.ToolTipDescription):
                case nameof(PresentationData.ControlData.Gestures):
                case nameof(PresentationData.ControlData.Command):
                case nameof(PresentationData.ControlData.RoutedCommand):
                    this.OnPropertyChanged(e.PropertyName);
                    break;
            }
        }

        public Input.CommandBinding CreateCommandBinding()
        {
            return this.controlData.CreateCommandBinding();
        }

        public override string ToString()
        {
            return this.path.ToString();
        }
    }
}
