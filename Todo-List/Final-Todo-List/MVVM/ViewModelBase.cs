using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Final_Todo_List.MVVM
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public void OnPropertyChange([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public class RelayCommand : ICommand
    {
        public Action<object> OnExecute;
        public Func<object, bool> _CanExecute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object> _execute, Func<object, bool> _canexecute = null)
        {
            OnExecute = _execute;
            _CanExecute = _canexecute;
        }
        public bool CanExecute(object? parameter)
        {
            return _CanExecute == null || _CanExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            OnExecute(parameter);
        }
    }
}
