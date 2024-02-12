using System.IO;
using System.Windows;
using System.Text.Json;

namespace Final_Todo_List.Model
{
    class DataManagerHandler
    {
        private readonly string _dataPath;
        private readonly string _appDataPath;

        public DataManagerHandler()
        {
            _dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Todo-List");
            _appDataPath = Path.Combine(_dataPath, "MyTodoList.json");
        }
        public List<Todo> Read()
        {
            List<Todo> list = new List<Todo>();
            if (Directory.Exists(_dataPath) && File.Exists(_appDataPath))
            {
                using (FileStream fs = File.Open(_appDataPath, FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string token = sr.ReadLine();
                        Todo deserializedToken = JsonSerializer.Deserialize<Todo>(token);
                        list.Add(deserializedToken);
                    }
                }

                return list;
            }
            else
            {
               MessageBoxResult boxResult = MessageBox.Show("No save data exist \n " +
                   "Create new save data?", "Save data not found",
                    MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (boxResult == MessageBoxResult.Yes)
                {
                    File.Create(_appDataPath);
                    Thread.Sleep(2000);
                    Read();
                }
            }
            return null;
        }
        public void Write(List<Todo> item)
        {
            if (item != null)
            {
                using (FileStream fs = File.Open(_appDataPath, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach(Todo token in item)
                    {
                        string serializedToken = JsonSerializer.Serialize(token);
                        sw.WriteLine(serializedToken);
                    }
                    
                }
                
            }
        }
    }
}
