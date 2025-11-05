using System;
using System.Windows.Input;

namespace TodoWPF.Commands
{
    // Basic command class for MVVM buttons
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;           // What to do
        private readonly Func<object?, bool>? _canExecute;   // When allowed

        // Raised when the button's enabled state changes
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Constructor: takes the execute action and optional can execute condition
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Decide if the command can run (default = true)
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute!(parameter);
        }

        // Perform the actual action
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
