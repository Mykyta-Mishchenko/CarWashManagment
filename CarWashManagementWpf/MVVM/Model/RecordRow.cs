using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CarWashManagementWpf.MVVM.Model
{
    class RecordRow
    {
        private int id;
        private string serviceType;
        private double price;
        private DateTime date;
        private string workers;
        public int ID { get { return id; }set { id = value; } }
        public string ServiceType { get {  return serviceType; } set {  serviceType = value; } }
        public double Price { get { return price; } set { price = value; } }
        public DateTime Date { get { return date; } set { date = value; } }
        public string Workers { get { return workers; } set { workers = value; } }
    }
}
