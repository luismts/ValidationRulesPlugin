using System;
using System.Windows.Input;

namespace Plugin.ValidationRules.Extensions
{
	public class RelayCommand : ICommand
	{
		readonly Func<object, bool> _canExecute;
		readonly Action<object> _execute;

		public RelayCommand(Action<object> execute)
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));

			_execute = execute;
		}

		public RelayCommand(Action execute) : this(o => execute())
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));
		}

		public RelayCommand(Action<object> execute, Func<object, bool> canExecute) : this(execute)
		{
			if (canExecute == null)
				throw new ArgumentNullException(nameof(canExecute));

			_canExecute = canExecute;
		}

		public RelayCommand(Action execute, Func<bool> canExecute) : this(o => execute(), o => canExecute())
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));
			if (canExecute == null)
				throw new ArgumentNullException(nameof(canExecute));
		}

        public bool CanExecute(object parameter)
		{
			if (_canExecute != null)
				return _canExecute(parameter);

			return true;
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}

		public event EventHandler CanExecuteChanged;

        public void ChangeCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
