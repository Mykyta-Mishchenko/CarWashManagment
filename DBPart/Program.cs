using ConsoleApp;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;


DBConection db =  new DBConection();

db.AddRecord("Car body wash", "Mykola K.,Nazar L.");


DataTable table = db.GetRecordTable();
foreach (DataRow row in table.Rows)
{
    foreach(DataColumn column in table.Columns)
    {
        Console.Write("{0,-20}",row[column]);
    }
    Console.WriteLine();
}
Console.WriteLine("||||||||Bill||||||||");
Dictionary<string, float> bill = db.GetWorkersBill();
foreach(var item in bill)
{
    Console.WriteLine("{0,-10}: {1,5}",item.Key,item.Value);
}