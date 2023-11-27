using CarWashManagementWpf.Core;
using CarWashManagementWpf.MVVM.Model;
using CarWashManagementWpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashManagementWpf.MVVM.ViewModel
{
    class WholeTableViewModel : Core.ViewModel
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

        public ObservableCollection<RecordRow> records { get; set; }

        public RelayCommand NavigateToMoneySplit { get; set; }
        public RelayCommand NavigateToSelection { get; set; }

        public void OnDataChanged(object sender, EventArgs e)
        {
            records = BindInstance.BindInstance.GetRecordList();
        }
        public WholeTableViewModel(INavigationService navigation, IBindService bind)
        {
            BindInstance = bind;
            Navigation = navigation;
            BindInstance.BindInstance.DBConnection.DataChanged += OnDataChanged;
            NavigateToSelection = new RelayCommand(execute: o => Navigation.NavigateTo<SelectionViewModel>(), canExecute: o => true);
            NavigateToMoneySplit = new RelayCommand(execute: o => Navigation.NavigateTo<MoneySplitViewModel>(), canExecute: o => true);
            records = BindInstance.BindInstance.GetRecordList();
            /*records = new ObservableCollection<Wholegridtest>()
            {
                new Wholegridtest()
                {
                    ID = 1,
                    WorkType = "utility",
                    Date = DateTime.Now,
                    Price = 250,
                    Workers = "one"
                },
                new Wholegridtest()
                {
                    ID = 2,
                    WorkType = "utility",
                    Date = DateTime.Now,
                    Price = 250,
                    Workers = "one"
                },
                new Wholegridtest()
                {
                    ID = 3,
                    WorkType = "utility",
                    Date = DateTime.Now,
                    Price = 250,
                    Workers = "one"
                }
            };*/
        }
    }
}
