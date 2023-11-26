using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashManagementWpf.MVVM.Model
{
    public class Worker
    {
        private string name;
        private float salary;
        

        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        public float Salary
        {
            get { return (float) Math.Round(salary, 2); }
            set { salary = value; }
        }
    }
}
