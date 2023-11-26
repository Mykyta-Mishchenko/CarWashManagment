using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace CarWashManagementWpf.MVVM.Model
{
    public class Bind
    {
        private DataTable recordsTable;
        private DBConnection dbConnection;

        public ObservableCollection<RecordRow> GetRecordList()
        {
            dbConnection = new DBConnection();
            ObservableCollection<RecordRow> records = new ObservableCollection<RecordRow>();
            recordsTable = dbConnection.GetRecordTable();

            foreach (DataRow row in recordsTable.Rows)
            {
                records.Add(new RecordRow
                {
                    ID = (int)row["ID"],
                    ServiceType = (string)row["Service_Type"],
                    Price = double.Parse((string)row["Service_Price"]),
                    // Price = (float)row["Service_Price"],
                    //Date = (DateTime)row["Service_Date"],
                    Date = DateTime.Parse((string)row["Service_Date"]),

                    Workers = (string)row["Service_Workers"]
                });
            }

            return records;
        }

        public ObservableCollection<Worker> GetWorkersBill()
        {
            dbConnection = new DBConnection();
            Dictionary<string, float> workersFromDB = dbConnection.GetWorkersBill();
            ObservableCollection<Worker> workersList = new ObservableCollection<Worker>();

            foreach (var worker in workersFromDB)
            {
                workersList.Add(new Worker
                {
                    Name = worker.Key,
                    Salary = worker.Value
                });
            }

            return workersList;
        }

        public double GetTotalBill()
        {
            dbConnection = new DBConnection();
            Dictionary<string, float> workersFromDB = dbConnection.GetWorkersBill();
            double totalBill = 0;
            foreach (var worker in workersFromDB)
            {
                totalBill += worker.Value;
            }

            return (double)Math.Round(totalBill, 2);
        }
        
        public ObservableCollection<string> GetWorkers()
        {
            dbConnection = new DBConnection();
            ObservableCollection<string> workers = new ObservableCollection<string>();

            foreach (var worker in dbConnection.GetAllWorkers())
            {
                workers.Add(worker);
            }

            return workers;
        }
    }
}
