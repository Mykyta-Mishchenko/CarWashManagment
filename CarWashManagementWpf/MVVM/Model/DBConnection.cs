using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarWashManagementWpf.MVVM.Model
{
    public class DBConnection
    {
        private string MySQLCon = "server=sql8.freesqldatabase.com;database=sql8663313;user=sql8663313;password=Ms3E9BJuXU";
        private MySqlConnection MySQLConnection;
        public DBConnection()
        {
            //Console.WriteLine("Start");
            MySQLConnection = new MySqlConnection(MySQLCon);
            try
            {
                MySQLConnection.Open();
                //Console.WriteLine("Connection is opened");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                MySQLConnection.Close();
            }
            //Console.WriteLine("End");
            UpdateDataBase();
        }
        public DataTable GetRecordTable()
        {
            DataTable recordTable = new DataTable("Records");
            DataColumn[] cols =
            {
                new DataColumn("ID", typeof(Int32)),
                new DataColumn("UNIQUE_ID", typeof(Int32)),
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
                MySqlCommand cmd = MySQLConnection.CreateCommand();
                cmd.CommandText = "SELECT ServiceTotal.id, ServiceTotal.service_id, ServiceTotal.workers," +
                    " ServiceTotal.date, ServiceTypes.service_type," +
                    " ServiceTypes.service_price FROM ServiceTotal, ServiceTypes" +
                    " WHERE ServiceTypes.service_id = ServiceTotal.service_id " +
                    " AND ServiceTotal.date >= CURDATE()";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    for (int i = 1; reader.Read(); ++i)
                    {
                        DataRow row = recordTable.NewRow();
                        row["ID"] = i;
                        row["UNIQUE_ID"] = reader.GetInt32("id");
                        row["Service_ID"] = reader.GetInt32("service_id");
                        row["Service_Type"] = reader.GetString("service_type");
                        row["Service_Price"] = reader.GetInt32("service_price");
                        string date = reader.GetString("date");
                        DateTime inputDateTime = DateTime.ParseExact(date, "dd.MM.yyyy HH:mm:ss", null);
                        date = inputDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        row["Service_Date"] = date;
                        row["Service_Workers"] = reader.GetString("workers");
                        recordTable.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { MySQLConnection.Close(); }
            return recordTable;
        }
        public void AddRecord(string service_type, string workers)
        {
            MySQLConnection = new MySqlConnection(MySQLCon);
            MySQLConnection.Open();

            int service_id = 0;
            string workers_IDs = "";
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

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
                workers = workers.Replace(',','/');
                workers_IDs = workers_IDs.TrimEnd(',');
                cmd.CommandText = "INSERT INTO ServiceTotal (service_id,date,workers) VALUES (" +
                                   service_id + ",'" + formattedDateTime + "','" + workers + "')";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { MySQLConnection.Close(); OnDataChanged(); }
        }
        public void ChangeDate(string date, int id)
        {
            try
            {
                MySQLConnection = new MySqlConnection(MySQLCon);
                MySQLConnection.Open();

                MySqlCommand cmd = MySQLConnection.CreateCommand();
                Debug.WriteLine(date);
                Debug.WriteLine(id);
                cmd.CommandText = "UPDATE ServiceTotal SET date='" + date + "' WHERE id = " + id;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally { MySQLConnection.Close(); }
        }
        public void AddWorker(string name)
        {
            try
            {
                MySQLConnection = new MySqlConnection(MySQLCon);
                MySQLConnection.Open();

                MySqlCommand cmd = MySQLConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO ServiceWorkers (worker_name) VALUES ('" + name + "')";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally { MySQLConnection.Close(); OnDataChanged(); }
        }
        public void RemoveWorker(string name)
        {
            try
            {
                MySQLConnection = new MySqlConnection(MySQLCon);
                MySQLConnection.Open();

                MySqlCommand cmd = MySQLConnection.CreateCommand();
                cmd.CommandText = "DELETE FROM ServiceWorkers WHERE worker_name = '" + name + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally { MySQLConnection.Close(); OnDataChanged(); }
        }
        public List<string> GetAllWorkers()
        {
            List<string> workers = new List<string>();
            try
            {
                MySQLConnection = new MySqlConnection(MySQLCon);
                MySQLConnection.Open();

                MySqlCommand cmd = MySQLConnection.CreateCommand();
                cmd.CommandText = "SELECT worker_name FROM ServiceWorkers";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        workers.Add(reader.GetString("worker_name"));
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally { MySQLConnection.Close(); }
            return workers;
        }
        public Dictionary<string, float> GetWorkersBill()
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
                        workers.Add(reader.GetString("worker_name"), 0);
                    }
                }

                DataTable recordTable = GetRecordTable();
                foreach (DataRow row in recordTable.Rows)
                {
                    string[] rowWorkers = row["Service_Workers"].ToString().Split('/');
                    for (int i = 0; i < rowWorkers.Length; i++)
                    {
                        if (!workers.ContainsKey(rowWorkers[i].Trim())) continue;
                        workers[rowWorkers[i].Trim()] += Convert.ToSingle(row["Service_Price"]) / rowWorkers.Length;
                    }
                }
                foreach(KeyValuePair<string, float> worker in workers)
                {
                    workers[worker.Key] *= 0.5f;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally { MySQLConnection.Close(); }
            return workers;
        }
        private void UpdateDataBase()
        {
            try
            {
                MySQLConnection = new MySqlConnection(MySQLCon);
                MySQLConnection.Open();

                MySqlCommand cmd = MySQLConnection.CreateCommand();
                cmd.CommandText = "DELETE FROM ServiceTotal WHERE date < NOW() - INTERVAL 30 DAY";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally { MySQLConnection.Close(); OnDataChanged(); }
        }

        public event EventHandler DataChanged;

        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
