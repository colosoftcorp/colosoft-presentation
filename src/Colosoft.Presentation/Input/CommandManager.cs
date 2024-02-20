using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security;
using System.Windows.Input;

namespace Colosoft.Presentation.Input
{
#pragma warning disable
    public static class CommandManager
    {
        private static HybridDictionary _classInputBindings = new HybridDictionary();

        public static readonly RoutedEvent CanExecuteEvent;

        public static readonly RoutedEvent ExecutedEvent;

        public static void RegisterClassInputBinding(Type type, InputBinding inputBinding)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (inputBinding == null)
            {
                throw new ArgumentNullException(nameof(inputBinding));
            }

            lock (_classInputBindings.SyncRoot)
            {
                InputBindingCollection inputBindings = _classInputBindings[type] as InputBindingCollection;

                if (inputBindings == null)
                {
                    inputBindings = new InputBindingCollection();
                    _classInputBindings[type] = inputBindings;
                }

                inputBindings.Add(inputBinding);
            }
        }

        [SecurityCritical, SecurityTreatAsSafe]
        private static void ExecuteCommand(RoutedCommand routedCommand, object parameter, object target, InputEventArgs inputEventArgs)
        {
            routedCommand.Execute(parameter);
        }

        public static void TranslateInput(object targetElement, InputEventArgs inputEventArgs)
        {
            if (targetElement == null || inputEventArgs == null)
            {
                return;
            }

            ICommand command = null;
            object target = null;
            object parameter = null;

            IEnumerable<InputBinding> inputBindings = null;

            if (targetElement is IInputBindingsContainer container)
            {
                inputBindings = container.InputBindings;
            }
            else
            {
                inputBindings = targetElement as IEnumerable<InputBinding>;
            }

            if (inputBindings != null)
            {
                var inputBinding = inputBindings.FindMatch(targetElement, inputEventArgs);
                if (inputBinding != null)
                {
                    command = inputBinding.Command;
                    target = inputBinding.CommandTarget;
                    parameter = inputBinding.CommandParameter;
                }
            }

            if (command == null)
            {
                lock (_classInputBindings.SyncRoot)
                {
                    Type classType = targetElement.GetType();
                    while (classType != null)
                    {
                        var classInputBindings = _classInputBindings[classType] as InputBindingCollection;
                        if (classInputBindings != null)
                        {
                            InputBinding inputBinding = classInputBindings.FindMatch(targetElement, inputEventArgs);
                            if (inputBinding != null)
                            {
                                command = inputBinding.Command;
                                target = inputBinding.CommandTarget;
                                parameter = inputBinding.CommandParameter;
                                break;
                            }
                        }
                        classType = classType.BaseType;
                    }
                }
            }

            if (command != null)
            {
                if (target == null)
                {
                    target = targetElement;
                }

                var continueRouting = true;

                var routedCommand = command as RoutedCommand;
                if (routedCommand != null)
                {
                    if (routedCommand.CanExecute(parameter))
                    {
                        continueRouting = false;
                        ExecuteCommand(routedCommand, parameter, target, inputEventArgs);
                    }
                }
                else
                {
                    if (command.CanExecute(parameter))
                    {
                        continueRouting = false;
                        command.Execute(parameter);
                    }
                }

                inputEventArgs.Handled = !continueRouting;
            }
        }
    }
#pragma warning restore
}
