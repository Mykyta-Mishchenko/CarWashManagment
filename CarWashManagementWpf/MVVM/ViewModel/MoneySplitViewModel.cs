using CarWashManagementWpf.Core;
using CarWashManagementWpf.MVVM.Model;
using CarWashManagementWpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarWashManagementWpf.MVVM.ViewModel
{
    class MoneySplitViewModel : Core.ViewModel
    {
        private double _total;

        public double Total
        {
            get { return _total; }
            set { _total = value; }
        }

        private IBindService _bindInstance;

        public IBindService BindInstance
        {
            get { return _bindInstance; }
            set { _bindInstance = value; }
        }
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }
        public RelayCommand NavigateToWholeTable { get; set; }

        public ObservableCollection<Worker> Workers { get; set; }
        public MoneySplitViewModel(INavigationService navigation, IBindService bind)
        {
            BindInstance = bind;
            Navigation = navigation;
            BindInstance.BindInstance.DBConnection.DataChanged += OnDataChanged;
            NavigateToWholeTable = new RelayCommand(execute: o => Navigation.NavigateTo<WholeTableViewModel>(), canExecute: o => true);
            Workers = BindInstance.BindInstance.GetWorkersBill();
            Total = BindInstance.BindInstance.GetTotalBill();

        }

        public void OnDataChanged(object sender, EventArgs e)
        {
            Workers = BindInstance.BindInstance.GetWorkersBill();
            Total = BindInstance.BindInstance.GetTotalBill();
        }

    }
}
