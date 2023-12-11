using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;

namespace CarWashManagementWpf.MVVM.Model
{
    public class Bind
    {
        private DataTable recordsTable;
        public DBConnection DBConnection { get; set; }
        public Bind() 
        {
            DBConnection = new DBConnection();
        }
        public ObservableCollection<RecordRow> GetRecordList()
        {
            ObservableCollection<RecordRow> records = new ObservableCollection<RecordRow>();
            recordsTable = DBConnection.GetRecordTable();
            foreach (DataRow row in recordsTable.Rows)
            {
                records.Add(new RecordRow
                {
                    ID = (int)row["ID"],
                    ServiceType = (string)row["Service_Type"],
                    Price = double.Parse((string)row["Service_Price"]),
                    // Price = (float)row["Service_Price"],
                    //Date = (DateTime)row["Service_Date"],
                    Date = (string)row["Service_Date"],
                    //DateTime.Parse((string)row["Service_Date"]),
                    Workers = (string)row["Service_Workers"]
                });
            }

            return records;
        }

        public ObservableCollection<Worker> GetWorkersBill()
        {
            Dictionary<string, float> workersFromDB = DBConnection.GetWorkersBill();
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
            Dictionary<string, float> workersFromDB = DBConnection.GetWorkersBill();
            double totalBill = 0;
            foreach (var worker in workersFromDB)
            {
                totalBill += worker.Value;
            }

            return (double)Math.Round(totalBill, 2);
        }
        
        public ObservableCollection<string> GetWorkers()
        {
            ObservableCollection<string> workers = new ObservableCollection<string>();

            foreach (var worker in DBConnection.GetAllWorkers())
            {
                workers.Add(worker);
            }

            return workers;
        }
        public void ChangeDate(int id, string newDate)
        {
            foreach (DataRow row in recordsTable.Rows)
            {
                if ((int)row["ID"] == id)
                {
                    Console.WriteLine(newDate);
                    DBConnection.ChangeDate(newDate, (int)row["UNIQUE_ID"]);
                    break;
                }
            }
        }
    }
}
