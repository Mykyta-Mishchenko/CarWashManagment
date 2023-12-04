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
    class WholeTableViewModel : Core.ViewModel
    {

        private RecordRow _selectedRow;

        public RecordRow SelectedRow
        {
            get { return _selectedRow; }
            set 
            {
                /*if (_selectedRow!= null && _selectedRow.ID == value.ID)
                {
                    if (SelectedRow.Date != value.Date)
                    {
                        _selectedRow = value;
                        OnPropertyChanged();
                        MessageBox.Show($"ID: {_selectedRow.ID}\nNew date {_selectedRow.Date}", "Data changed", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                } else
                {
                    _selectedRow = value;
                    OnPropertyChanged();
                }*/

                _selectedRow = value;
                OnPropertyChanged();
                //MessageBox.Show($"ID: {_selectedRow.ID}\nNew date {_selectedRow.Date}", "Data changed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        void OnDataTimeChanged(object sender, EventArgs e)
        {
            if (SelectedRow!=null)
            {

                BindInstance.BindInstance.ChangeDate(SelectedRow.ID, SelectedRow.Date.ToString());
                MessageBox.Show($"ID: {SelectedRow.ID}\nNew date: {SelectedRow.Date}", "Date changed");
                MessageBox.Show($"{records[0].ID} {records[0].Date}\n{records[1].ID} {records[1].Date}\n{records[2].ID} {records[2].Date}", "Date changed");
                records = BindInstance.BindInstance.GetRecordList();
                OnPropertyChanged(nameof(records));

            }
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
            RecordRow.DateTimeChanged += OnDataTimeChanged;
            
        }
    }
}
