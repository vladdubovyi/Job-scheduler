using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace Task_Manager
{
    public class TaskAdapter
    {
        private List<Task> _tasks;

        public TaskAdapter()
        {
            _tasks = new List<Task>();
        }

        public void AddTask(string name, string description, DateTime date, int priority = 0, bool status = false)
        {
            Task task = new Task();

            task._name = name;
            task._description = description;
            task._date = date;
            task._priority = priority;
            task._isDone = status;

            _tasks.Add(task);
        }
        public void AddTask(Task item)
        {
            _tasks.Add(item);
        }
        public void RemoveTask(int index)
        {
            _tasks.RemoveAt(index);
        }
        public void Clear()
        {
            _tasks.Clear();
        }

        public void editName(string name, int index)
        {
            _tasks[index]._name = name;
        }
        public void editDescription(string desc, int index)
        {
            _tasks[index]._description = desc;
        }
        public void editDate(DateTime date, int index)
        {
            _tasks[index]._date = date;
        }
        public void editPriority(int priority, int index)
        {
            _tasks[index]._priority = priority;
        }
        public void editStatus(bool status, int index)
        {
            _tasks[index]._isDone = status;
        }

        public List<Task> GetTasks()
        {
            return _tasks;
        }

        public void Save()
        {
            using (SaveFileDialog dlg = new SaveFileDialog() { Filter = "Json Files|*.json" })
                if(dlg.ShowDialog() == DialogResult.OK)
                    using (StreamWriter writer = new StreamWriter(dlg.FileName))
                        writer.Write(JsonSerializer.Serialize<List<Task>>(_tasks));
        }
        public void Load()
        {
            using (OpenFileDialog dlg = new OpenFileDialog() { Filter = "Json Files|*.json" })
                if (dlg.ShowDialog() == DialogResult.OK)
                    using (StreamReader reader = new StreamReader(dlg.FileName))
                        _tasks = JsonSerializer.Deserialize<List<Task>>(reader.ReadToEnd());
        }
    }
}
