using Final_Todo_List.MVVM;

namespace Final_Todo_List.Model
{
    class Todo : ViewModelBase
    {
        public string TodoList { get; set; }
        public string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChange(); }
        }

        public Todo()
        {

        }
        public Todo(string todo, string status = "Pending")
        {
            TodoList = todo;
            Status = status;
        }
    }
}
