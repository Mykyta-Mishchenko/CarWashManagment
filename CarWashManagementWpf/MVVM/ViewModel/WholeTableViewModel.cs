using CarWashManagementWpf.Core;
using CarWashManagementWpf.MVVM.Model;
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
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Wholegridtest> records { get; set; }

        public RelayCommand NavigateToMoneySplit { get; set; }
        public RelayCommand NavigateToSelection { get; set; }
        public WholeTableViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            NavigateToSelection = new RelayCommand(execute: o => Navigation.NavigateTo<SelectionViewModel>(), canExecute: o => true);
            NavigateToMoneySplit = new RelayCommand(execute: o => Navigation.NavigateTo<MoneySplitViewModel>(), canExecute: o => true);
            records = new ObservableCollection<Wholegridtest>()
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
            };
            records.Add(new Wholegridtest()
            {
                ID = 3,
                WorkType = "utility",
                Date = DateTime.Now,
                Price = 250,
                Workers = "one"
            });
        }
    }
}
