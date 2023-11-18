using CarWashManagementWpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface INavigationService 
{
    ViewModel CurrentView { get; }
    void NavigateTo<TViewModel>() where TViewModel : ViewModel;
}

namespace CarWashManagementWpf.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private ViewModel _currentView;
        private Func<Type, ViewModel> _viewModelFactory;

        public ViewModel CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public NavigationService(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }
    }
}
