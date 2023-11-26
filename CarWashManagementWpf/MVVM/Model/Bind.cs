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
        private DbConnection dbConnection;

        public ObservableColection<RecordRow> GetRecordList()
        {

        }

        public ObservableCollection<Worker> GetTotalBill()
        {
            ObservableCollection<Worker> workers = 
        }
    }
}
