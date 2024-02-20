using System;
using System.ComponentModel;
using System.Linq;

namespace Colosoft.Presentation.Menu
{
    public class MenuItem : IMenuItem
    {
        public const string MenuScheme = "menu";

        public static Func<MenuItem, Input.IRoutedCommand> RoutedCommandConverter { get; set; } = (menuItem) => new MenuItemCommand(menuItem);

        private static readonly System.Windows.Input.ICommand EmptyCommand =
            new Input.DelegateCommand(() => { }, () => true);

        private readonly string name;
        private Uri path;
        private IMessageFormattable label;
        private Uri largeImage;
        private Uri smallImage;
        private IMessageFormattable toolTipTitle;
        private IMessageFormattable toolTipDescription;
        private System.Windows.Input.ICommand command;
        private object commandParameter;
        private string gestures;

        private Input.IRoutedCommand routedCommand;

        private IMenuPosition position;

        public event PropertyChangedEventHandler PropertyChanged;

        public MenuItem(Uri path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (path.Scheme != MenuScheme)
            {
                throw new InvalidOperationException(
                    ResourceMessageFormatter.Create(() => Properties.Resources.MenuItem_InvalidPathScheme).Format());
            }

            this.position = new AbsolutePosition(-1);
            this.path = path;
            this.name = path.Segments.LastOrDefault(f => f != "/" && f != "\\") ?? path.Host;
        }

        public MenuItem(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.position = new AbsolutePosition(-1);
            this.name = name;
            this.path = new Uri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}://{1}", MenuScheme, name));
        }

        public string Name => this.name;

        public IMessageFormattable Label
        {
            get => this.label;
            set
            {
                if (this.label != value)
                {
                    this.label = value;
                    this.OnPropertyChanged(nameof(this.Label));
                }
            }
        }

        public Uri LargeImage
        {
            get
            {
                return this.largeImage;
            }

            set
            {
                if (this.largeImage != value)
                {
                    this.largeImage = value;
                    this.OnPropertyChanged(nameof(this.LargeImage));
                }
            }
        }

        public Uri SmallImage
        {
            get
            {
                return this.smallImage;
            }

            set
            {
                if (this.smallImage != value)
                {
                    this.smallImage = value;
                    this.OnPropertyChanged(nameof(this.SmallImage));
                }
            }
        }

        public IMessageFormattable ToolTipTitle
        {
            get
            {
                return this.toolTipTitle;
            }

            set
            {
                if (this.toolTipTitle != value)
                {
                    this.toolTipTitle = value;
                    this.OnPropertyChanged(nameof(this.ToolTipTitle));
                }
            }
        }

        public IMessageFormattable ToolTipDescription
        {
            get
            {
                return this.toolTipDescription;
            }

            set
            {
                if (this.toolTipDescription != value)
                {
                    this.toolTipDescription = value;
                    this.OnPropertyChanged(nameof(this.ToolTipDescription));
                }
            }
        }

        public string Gestures
        {
            get { return this.gestures; }
            set
            {
                if (this.gestures != value)
                {
                    this.gestures = value;
                    this.OnPropertyChanged(nameof(this.Gestures));
                }
            }
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
            get
            {
                return this.command ?? EmptyCommand;
            }

            set
            {
                if (this.command != value)
                {
                    this.command = value;
                    this.routedCommand = null;
                    this.OnPropertyChanged(
                        nameof(this.Command),
                        nameof(this.RoutedCommand));
                }
            }
        }

        public object CommandParameter
        {
            get => this.commandParameter;
            set
            {
                if (this.commandParameter != value)
                {
                    this.commandParameter = value;
                    this.OnPropertyChanged(nameof(this.commandParameter));
                }
            }
        }

        public Input.IRoutedCommand RoutedCommand
        {
            get
            {
                if (this.routedCommand == null)
                {
                    if (RoutedCommandConverter == null)
                    {
                        throw new InvalidOperationException("RoutedCommandConverter not defined.");
                    }

                    this.routedCommand = RoutedCommandConverter(this);
                }

                return this.routedCommand;
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

        public Input.CommandBinding CreateCommandBinding()
        {
            return new Input.CommandBinding(
                this.RoutedCommand,
                (x, e) => this.Command.Execute(e.Parameter),
                (x, e) => e.CanExecute = this.Command.CanExecute(e.Parameter));
        }

        public override string ToString()
        {
            return this.path.ToString();
        }
    }
}
