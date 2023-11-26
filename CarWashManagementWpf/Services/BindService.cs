using CarWashManagementWpf.Core;
using CarWashManagementWpf.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashManagementWpf.Services
{
    public interface IBindService
    {
        public Bind BindInstance { get; set; }
    }
    public class BindService : ObservableObject, IBindService
    {
        public Bind BindInstance { get; set; }
    }
    
}