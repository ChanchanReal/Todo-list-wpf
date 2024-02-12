using Final_Todo_List.Model;
using Final_Todo_List.MVVM;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
namespace Final_Todo_List.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private string _todoList;
        private bool txtPlaceHolder = false;
        private DataManagerHandler _dataHandler;
        public ObservableCollection<Todo> TodoListCollection { get; set; }
        public RelayCommand DeleteItem { get; set; }
        public RelayCommand AddTodo => new RelayCommand(_execute => AddItemTODO());
        public RelayCommand SaveBeforeClose => new RelayCommand(_execute => SaveOnClose());
        public RelayCommand UpdateStatus { get; set; }
        public MainWindowViewModel()
        {
            _dataHandler = new DataManagerHandler();

            TodoListCollection = new ObservableCollection<Todo>(_dataHandler.Read());

            UpdateStatus = new RelayCommand(ChangeStatus);
            DeleteItem = new RelayCommand(DeleteSelectedItem);
        }
        private Todo selectedItem;

        public Todo SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChange(); }
        }


        public string TodoList
        {
            get { return _todoList; }
            set 
            { 
                _todoList = value; OnPropertyChange();
                if (!string.IsNullOrEmpty(TodoList))
                {
                    TxtPlaceHolder = true;
                }
                else
                {
                    TxtPlaceHolder = false;
                }
            }
        }
        public bool TxtPlaceHolder
        {
            get { return txtPlaceHolder; }
            set { txtPlaceHolder = value; OnPropertyChange(); }
        }
        public void DeleteSelectedItem(object todo)
        {
            if(todo is Todo)
            {
                Todo itemSelected = (Todo)todo;
                TodoListCollection.Remove(itemSelected);
            }
        }

        public void SaveOnClose()
        {
            Task task = SaveClose();
            task.Wait();
        }
        public void Save()
        {
            _dataHandler.Write(TodoListCollection.ToList());
        }
        public Task SaveClose()
        {
            return Task.Run(() =>
            {
                Save();
            });
        }

        public void AddItemTODO()
        {
            IEnumerable<string> item = from o in TodoListCollection
                                       where o.TodoList == TodoList 
                                       select o.TodoList;

            if (!item.Contains<string>(TodoList))
            {
                if (!string.IsNullOrEmpty(TodoList))
                {
                    TodoListCollection.Add(new Todo(TodoList));

                    _dataHandler.Write(TodoListCollection.ToList());
                }
                
            }
            else
            {
                    MessageBox.Show(TodoList, "Already exist!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
           
        }

        public void ChangeStatus(object selectedItem)
        {
            if (selectedItem is Todo todo)
            {
                todo.Status = todo.Status switch
                {
                    "Pending" => "Waiting",
                    "Waiting" => "Complete",
                    "Complete" => "Canceled",
                    "Canceled" => "Pending",
                    _ => throw new NotImplementedException()
                };
            }
        }
        public enum StatusTodo { Complete, Waiting, Canceled, Pending }
    }
}


