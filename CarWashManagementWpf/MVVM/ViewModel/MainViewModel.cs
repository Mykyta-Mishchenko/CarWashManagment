using CarWashManagementWpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashManagementWpf.MVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }
        public RelayCommand NavigateToMoneySplit { get; set; }
        public RelayCommand NavigateToWholeTable { get; set; }
        public RelayCommand NavigateToSelection { get; set; }
        public MainViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            NavigateToSelection = new RelayCommand(execute: o => Navigation.NavigateTo<SelectionViewModel>(), canExecute: o => true);
            NavigateToWholeTable = new RelayCommand(execute: o => Navigation.NavigateTo<WholeTableViewModel>(), canExecute: o => true);
            NavigateToMoneySplit = new RelayCommand(execute: o => Navigation.NavigateTo<MoneySplitViewModel>(), canExecute: o => true);
            Navigation.NavigateTo<SelectionViewModel>();
        }
    }
}
