using Xamarin.Forms;
using MoveUp.Services;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using MoveUp.Installers;
using MoveUp.Views;
using System.Collections.Generic;
using MoveUp.Factories.Interfaces;
using System;

namespace MoveUp
{
    public partial class App : Application
    {
        public IDependencyInjectionService MyDependencyService { get; }

        public App(IEnumerable<IInstaller> remoteInstallers = null, int startupPage = 0)
        {
            InitializeComponent();

            MyDependencyService = new DependencyInjectionService();
            var navigationPage = new NavigationPage();

            RegisterDependencies(MyDependencyService, navigationPage.Navigation, remoteInstallers);
            var navigationService = MyDependencyService.Resolve<INavigationService>();

            if (startupPage == 0 || startupPage == 1)
                navigationService.PushAsync<SummaryViewModel>();
            else if (startupPage == 2)
                navigationService.PushAsync<ActivitiesViewModel>();
            else if (startupPage == 3)
                navigationService.PushAsync<FriendsViewModel>();

            MainPage = navigationPage;
        }

        private void RegisterDependencies(IDependencyInjectionService dependencyService, INavigation navigation, IEnumerable<IInstaller> remoteInstallers)
        {
            var serviceCollection = new ServiceCollection();
            var coreInstaller = new CoreInstaller();

            coreInstaller.Install(serviceCollection, dependencyService, navigation);

            foreach (IInstaller installer in remoteInstallers)
            {
                installer.Install(serviceCollection);
            }

            dependencyService.Build(serviceCollection);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            MyDependencyService.Resolve<ITimerService>().StopTimer();
            MyDependencyService.Resolve<IStorageManager>().SavePositionsCache();
        }

        protected override void OnResume()
        {
            MyDependencyService.Resolve<ITimerService>().ResumeTimer();
        }
    }
}
