using CarWashManagementWpf.Core;
using CarWashManagementWpf.MVVM.Model;
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
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }
        public RelayCommand NavigateToWholeTable { get; set; }

        public ObservableCollection<Worker> Workers { get; set; }
        public MoneySplitViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            NavigateToWholeTable = new RelayCommand(execute: o => Navigation.NavigateTo<WholeTableViewModel>(), canExecute: o => true);
            Workers = new ObservableCollection<Worker>()
            {
                new Worker()
                {
                    Name="Petro",
                    Money=1800
                },
                new Worker() {
                    Name="Ivan",
                    Money=900
                }

            };
        }

    }
}
