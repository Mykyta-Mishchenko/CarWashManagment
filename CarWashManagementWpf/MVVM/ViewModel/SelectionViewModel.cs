using CarWashManagementWpf.Core;
using CarWashManagementWpf.MVVM.Model;
using CarWashManagementWpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarWashManagementWpf.MVVM.ViewModel
{
    class SelectionViewModel : Core.ViewModel
    {
        private ObservableCollection<string> _workers;

        public ObservableCollection<string> Workers
        {
            get { return _workers; }
            set 
            {
                _workers = value;
                OnPropertyChanged();
            }
        }

        private IBindService _bindInstance;

        public IBindService BindInstance
        {
            get { return _bindInstance; }
            set { _bindInstance = value; }
        }
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
        public RelayCommand SyncWorkersCommand { get; set; }
        public RelayCommand NextCommand { get; set; }
        public string WorkersString { get; set; }

        /// <summary>
        /// A method that takes the chosen workers and converts them to a string (the type of parametr used in DBConnection.AddRecord())
        /// </summary>
        /// <remarks>
        /// The result is saved in a WorkersString property
        /// </remarks>
        /// <param name="o"></param>
        /// <returns>true, if worker(s) were chosen and the conversion is successful, false othewvise</returns>
        bool SyncWorkersMethod(object o)
        {
            if (o == null)
            {
                MessageBox.Show("Please, choose workers", "Worker(s) was not chosen", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } else
            {
                System.Collections.IList items = o as System.Collections.IList;
                var collection = items.Cast<string>().ToList();
                WorkersString = string.Join(", ", collection);
                return true;
            }
        }

        bool AddRecord()
        {
            if (ChosenUtility != null && string.IsNullOrEmpty(WorkersString) == false)
            {
                BindInstance.BindInstance.DBConnection.AddRecord(ChosenUtility.Name, WorkersString);
                return true;
            }
            else
            {
                MessageBox.Show("Please, choose all the parameters", "Some parameters were not chosen", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        void AddAndNextPage(object o)
        {
            if (SyncWorkersMethod(o))
            {
                if (AddRecord())
                {
                    Navigation.NavigateTo<WholeTableViewModel>();
                }
            }
        }

        public SelectionViewModel(INavigationService navigation, IBindService bind)
        {
            BindInstance = bind;
            Navigation = navigation;
            NavigateToWholeTable = new RelayCommand(execute: o => Navigation.NavigateTo<WholeTableViewModel>(), canExecute: o => true);
            NavigateToMoneySplit = new RelayCommand(execute: o => Navigation.NavigateTo<MoneySplitViewModel>(), canExecute: o => true);
            //SyncWorkersCommand = new RelayCommand(execute: o => Navigation.NavigateTo<MoneySplitViewModel>(), canExecute: o => true);
            SyncWorkersCommand = new RelayCommand(execute: o => { SyncWorkersMethod(o); }, canExecute: o => true);
            NextCommand = new RelayCommand(execute: o => { AddAndNextPage(o); }, canExecute: o => true);
            Workers = BindInstance.BindInstance.GetWorkers();
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
