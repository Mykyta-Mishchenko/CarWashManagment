using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashManagementWpf.MVVM.Model
{
    public class Bind
    {
        private DataTable recordsTable;
        private DBConnection dbConnection;

        public ObservableCollection<RecordRow> GetRecordList()
        {

        
        }

        public ObservableCollection<Worker> GetTotalBill()
        {
            Dictionary<string, float> workersFromDB = dbConnection.GetWorkersBill();
            ObservableCollection<Worker> workersList = new ObservableCollection<Worker>();

            foreach(var worker in workersFromDB)
            {
                Worker workerFromDB = new Worker();
                workerFromDB.Name = worker.Key;
                workerFromDB.Money = worker.Value;

                workersList.Add(workerFromDB); 
            }           

            return workers;
        }
    }
}
