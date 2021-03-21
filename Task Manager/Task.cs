using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager
{
    [Serializable]
    public class Task
    {
        public string _name { get; set; }
        public string _description { get; set; }
        public DateTime _date { get; set; }
        public int _priority { get; set; }
        public bool _isDone { get; set; }
    }
}
