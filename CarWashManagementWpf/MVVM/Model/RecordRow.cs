using CarWashManagementWpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarWashManagementWpf.MVVM.Model
{
    public class RecordRow : ObservableObject
    {
        private int id;
        private string serviceType;
        private double price;
        private string date;
        private string workers;
        public int ID { get { return id; }set { id = value; } }
        public string ServiceType { get {  return serviceType; } set {  serviceType = value; } }
        public double Price { get { return price; } set { price = value; } }
        public string Date 
        {
            get { return date; } 
            set 
            {
                if (date != DateTime.MinValue.ToString())
                {
                    date = value;
                    OnDateTimeChanged();
                }
                else
                {
                    date = value;
                }
            } 
        }
        public string Workers { get { return workers; } set { workers = value; } }

        public static event EventHandler DateTimeChanged;

        protected virtual void OnDateTimeChanged()
        {
            DateTimeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
