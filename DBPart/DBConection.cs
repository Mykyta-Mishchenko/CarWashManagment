using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp
{
    public class DBConection
    {
        private string MySQLCon = "server=sql8.freesqldatabase.com;database=sql8663313;user=sql8663313;password=Ms3E9BJuXU";
        private MySqlConnection MySQLConnection;
        public DBConection()
        {
            Console.WriteLine("Start");
            MySQLConnection = new MySqlConnection(MySQLCon);
            try
            {
                MySQLConnection.Open();
                Console.WriteLine("Connection is opened");
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.Message);
            }
            finally{
                MySQLConnection.Close();
            }
            Console.WriteLine("End");
        }
        public bool AddTableRecord()
        {

            return true;
        }
        public DataTable GetRecordTable()
        {
            DataTable recordTable = new DataTable("Records");
            DataColumn[] cols =
            {
                new DataColumn("ID", typeof(Int32)),
                new DataColumn("Service_ID", typeof(Int32)),
                new DataColumn("Service_Type", typeof(string)),
                new DataColumn("Service_Price", typeof(string)),
                new DataColumn("Service_Date", typeof(string)),
                new DataColumn("Service_Workers", typeof(string))
            };
            recordTable.Columns.AddRange(cols);
            MySQLConnection = new MySqlConnection(MySQLCon);
            MySQLConnection.Open();
            try
            {
                MySqlCommand records = MySQLConnection.CreateCommand();
                records.CommandText = "SELECT id FROM ServiceTotal";
                List<int> records_IDs = new List<int>();
                using (MySqlDataReader reader = records.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records_IDs.Add(reader.GetInt32(0));
                    }
                }
                MySqlCommand cmd = MySQLConnection.CreateCommand();
                for (int i = 0; i < records_IDs.Count(); i++)
                {
                    cmd.CommandText = "SELECT ServiceTotal.id, ServiceTotal.service_id, ServiceTotal.workers_id," +
                    " ServiceTotal.date,ServiceWorkers.worker_id ,ServiceWorkers.worker_name, ServiceTypes.service_type," +
                    " ServiceTypes.service_price FROM ServiceTotal, ServiceTypes, ServiceWorkers" +
                    " WHERE ServiceTypes.service_id = ServiceTotal.service_id AND ServiceTotal.id = " + records_IDs[i];
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataRow row = recordTable.NewRow();
                        bool flag = false;
                        while (reader.Read())
                        {
                            if (reader.GetString("workers_id").Contains(reader.GetString("worker_id")))
                            {
                                if (flag)
                                {
                                    row["Service_Workers"] += " / " + reader.GetString("worker_name");
                                }
                                else
                                {
                                    row["ID"] = i;
                                    row["Service_ID"] = reader.GetInt32("service_id");
                                    row["Service_Type"] = reader.GetString("service_type");
                                    row["Service_Price"] = reader.GetInt32("service_price");
                                    row["Service_Date"] = reader.GetString("date");
                                    row["Service_Workers"] = reader.GetString("worker_name");
                                    flag = true;
                                }
                            }
                        }
                        recordTable.Rows.Add(row);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally{ MySQLConnection.Close(); }
            return recordTable;
        }
        public void AddRecord(string service_type, string workers)
        {
            MySQLConnection = new MySqlConnection(MySQLCon);
            MySQLConnection.Open();

            int service_id = 0;
            string workers_IDs = "";
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM--dd HH:mm:ss");

            try
            {
                MySqlCommand cmd = MySQLConnection.CreateCommand();
                cmd.CommandText = "SELECT service_id, service_price FROM ServiceTypes WHERE service_type='" + service_type + "'";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        service_id = reader.GetInt32("service_id");
                    }
                }
                string[] workersNames = workers.Split(',');
                for (int i = 0; i < workersNames.Length; i++)
                {
                    cmd.CommandText = "SELECT worker_id FROM ServiceWorkers WHERE worker_name='" + workersNames[i]+"'";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            workers_IDs += reader.GetInt32("worker_id")+",";
                        }
                    }
                }
                workers_IDs = workers_IDs.TrimEnd(',');
                cmd.CommandText = "INSERT INTO ServiceTotal (service_id,date,workers_id) VALUES ("+
                                   service_id+",'"+ formattedDateTime+"','"+workers_IDs+"')";
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { MySQLConnection.Close(); }
        }
        public void ChangeDate(string date, int id)
        {
            try
            {
                MySQLConnection = new MySqlConnection(MySQLCon);
                MySQLConnection.Open();

                MySqlCommand cmd = MySQLConnection.CreateCommand();
                cmd.CommandText = "UPDATE ServiceTotal Set date='" + date + "' WHERE id = " + id;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { MySQLConnection.Close(); }
        }
        public void AddWorker(string name)
        {
            try
            {
                MySQLConnection = new MySqlConnection(MySQLCon);
                MySQLConnection.Open();

                MySqlCommand cmd = MySQLConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO ServiceWorkers (worker_name) VALUES ('"+name+"')";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { MySQLConnection.Close(); }
        }
        public void RemoveWorker(string name)
        {
            try
            {
                MySQLConnection = new MySqlConnection(MySQLCon);
                MySQLConnection.Open();

                MySqlCommand cmd = MySQLConnection.CreateCommand();
                cmd.CommandText = "DELETE FROM ServiceWorkers WHERE worker_name = '"+name+"'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { MySQLConnection.Close(); }
        }
        public Dictionary<string,float> GetWorkersBill()
        {
            Dictionary<string, float> workers = new Dictionary<string, float>();
            try
            {
                MySQLConnection = new MySqlConnection(MySQLCon);
                MySQLConnection.Open();

                MySqlCommand cmd = MySQLConnection.CreateCommand();
                cmd.CommandText = "SELECT worker_name FROM ServiceWorkers";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        workers.Add(reader.GetString("worker_name"),0);
                    }
                }

                DataTable recordTable = GetRecordTable();
                foreach (DataRow row in recordTable.Rows)
                {
                    string []rowWorkers = row["Service_Workers"].ToString().Split('/');
                    for (int i = 0; i < rowWorkers.Length; i++)
                    {
                        workers[rowWorkers[i].Trim()] += Convert.ToSingle(row["Service_Price"]) / rowWorkers.Length;
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { MySQLConnection.Close(); }
            return workers;
        }
    }
}
