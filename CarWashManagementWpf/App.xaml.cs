using CarWashManagementWpf.Core;
using CarWashManagementWpf.MVVM.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CarWashManagementWpf
{
        /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider =>new MainWindow()
            {
                DataContext = provider.GetRequiredService<MainViewModel>()   
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<SelectionViewModel>();
            services.AddSingleton<MoneySplitViewModel>();
            services.AddSingleton<WholeTableViewModel>();
            services.AddSingleton<INavigationService, Services.NavigationService>();



            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
