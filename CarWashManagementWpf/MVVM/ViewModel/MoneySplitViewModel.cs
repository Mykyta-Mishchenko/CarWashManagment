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
            NavigateToWholeTable = new RelayCommand(execute: o => Navigation.NavigateTo<WholeTableViewModel>(), canExecute: o => true);
            Workers = BindInstance.BindInstance.GetWorkersBill();
            /*Workers = new ObservableCollection<Worker>()
            {
                new Worker()
                {
                    Name="Petro",
                    Salary=1800
                },
                new Worker() {
                    Name="Ivan",
                    Salary=900
                }

            };*/
        }

    }
}
