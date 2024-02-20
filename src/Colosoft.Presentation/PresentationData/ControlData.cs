using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Colosoft.Presentation.PresentationData
{
    public class ControlData : IConfigurableCommandData, IGestureCommandData
    {
        private string name;
        private IMessageFormattable label;
        private Uri largeImage;
        private Uri smallImage;
        private IMessageFormattable toolTipTitle;
        private IMessageFormattable toolTipDescription;
        private Uri toolTipImage;
        private IMessageFormattable toolTipFooterTitle;
        private IMessageFormattable toolTipFooterDescription;
        private Uri toolTipFooterImage;
        private ICommand command;
        private Input.IRoutedCommand routedCommand;
        private string keyTip;
        private string gestures;
        private object dataContext;

        public event PropertyChangedEventHandler PropertyChanged;

        public ControlData()
        {
        }

        public ControlData(Action executeMethod, Func<bool> canExecuteMethod = null)
        {
            this.command = new Input.DelegateCommand(executeMethod, canExecuteMethod);
        }

        public ControlData(Func<CancellationToken, Task> executeMethod, Func<bool> canExecuteMethod = null, bool useNewThread = false)
        {
            this.command = new Input.AwaitableDelegateCommand(executeMethod, canExecuteMethod)
            {
                UseNewThread = useNewThread,
            };
        }

        public ControlData(Action<object> executeMethod, Func<object, bool> canExecuteMethod = null)
        {
            this.command = new Input.DelegateCommand<object>(executeMethod, canExecuteMethod);
        }

        public ControlData(Func<object, CancellationToken, Task> executeMethod, Func<object, bool> canExecuteMethod = null, bool useNewThread = false)
        {
            this.command = new Input.AwaitableDelegateCommand<object>(executeMethod, canExecuteMethod)
            {
                UseNewThread = useNewThread,
            };
        }

        public string Name
        {
            get => this.name;
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged(nameof(this.Name));
                }
            }
        }

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
            get => this.largeImage;
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
            get => this.smallImage;
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
            get => this.toolTipTitle;

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
            get => this.toolTipDescription;

            set
            {
                if (this.toolTipDescription != value)
                {
                    this.toolTipDescription = value;
                    this.OnPropertyChanged(nameof(this.ToolTipDescription));
                }
            }
        }

        public Uri ToolTipImage
        {
            get => this.toolTipImage;

            set
            {
                if (this.toolTipImage != value)
                {
                    this.toolTipImage = value;
                    this.OnPropertyChanged(nameof(this.ToolTipImage));
                }
            }
        }

        public IMessageFormattable ToolTipFooterTitle
        {
            get => this.toolTipFooterTitle;
            set
            {
                if (this.toolTipFooterTitle != value)
                {
                    this.toolTipFooterTitle = value;
                    this.OnPropertyChanged(nameof(this.ToolTipFooterTitle));
                }
            }
        }

        public IMessageFormattable ToolTipFooterDescription
        {
            get => this.toolTipFooterDescription;
            set
            {
                if (this.toolTipFooterDescription != value)
                {
                    this.toolTipFooterDescription = value;
                    this.OnPropertyChanged(nameof(this.ToolTipFooterDescription));
                }
            }
        }

        public Uri ToolTipFooterImage
        {
            get => this.toolTipFooterImage;

            set
            {
                if (this.toolTipFooterImage != value)
                {
                    this.toolTipFooterImage = value;
                    this.OnPropertyChanged(nameof(this.ToolTipFooterImage));
                }
            }
        }

        public string Gestures
        {
            get => this.gestures;
            set
            {
                if (this.gestures != value)
                {
                    this.gestures = value;
                    this.OnPropertyChanged(nameof(this.Gestures));
                }
            }
        }

        public object DataContext
        {
            get => this.dataContext;
            set
            {
                if (this.dataContext != value)
                {
                    this.dataContext = value;
                    this.OnPropertyChanged(nameof(this.DataContext));
                }
            }
        }

        public ICommand Command
        {
            get => this.command;
            set
            {
                if (this.command != value)
                {
                    this.command = value;
                    this.OnPropertyChanged(nameof(this.Command));
                }
            }
        }

        public Input.IRoutedCommand RoutedCommand
        {
            get => this.routedCommand;
            set
            {
                if (this.routedCommand != value)
                {
                    this.routedCommand = value;
                    this.OnPropertyChanged(nameof(this.RoutedCommand));
                }
            }
        }

        public string KeyTip
        {
            get => this.keyTip;

            set
            {
                if (this.keyTip != value)
                {
                    this.keyTip = value;
                    this.OnPropertyChanged(nameof(this.KeyTip));
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
