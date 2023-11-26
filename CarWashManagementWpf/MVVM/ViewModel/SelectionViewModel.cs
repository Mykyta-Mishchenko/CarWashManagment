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
    class SelectionViewModel : Core.ViewModel
    {
        public ObservableCollection<Utility> utilities { get; set; }
        private Utility _chosenUtility;

        public Utility ChosenUtility
        {
            get { return _chosenUtility; }
            set { _chosenUtility = value; OnPropertyChanged(); }
        }

        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }
        public RelayCommand NavigateToMoneySplit { get; set; }
        public RelayCommand NavigateToWholeTable { get; set; }

        public SelectionViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            NavigateToWholeTable = new RelayCommand(execute: o => Navigation.NavigateTo<WholeTableViewModel>(), canExecute: o => true);
            NavigateToMoneySplit = new RelayCommand(execute: o => Navigation.NavigateTo<MoneySplitViewModel>(), canExecute: o => true);
            utilities = new ObservableCollection<Utility>()
            {
                new Utility()
                {
                    Name = "Car body wash",
                    ImageSource = "../Images/car-wash.jpg",
                    Price = "$250"
                },
                new Utility()
                {
                    Name = "Interior and body",
                    ImageSource = "../Images/car-wash.jpg",
                    Price = "$350"
                },
                new Utility()
                {
                    Name = "Dry cleaning",
                    ImageSource = "../Images/car-wash.jpg",
                    Price = "$1800"
                }
            };
        }
    }
}
