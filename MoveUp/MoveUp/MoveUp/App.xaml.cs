using Xamarin.Forms;
using MoveUp.Services;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using MoveUp.Installers;
using MoveUp.Views;
using System.Collections.Generic;
using MoveUp.Factories.Interfaces;

namespace MoveUp
{
    public partial class App : Application
    {
        public IDependencyInjectionService MyDependencyService { get; }

        public App(IEnumerable<IInstaller> remoteInstallers = null)
        {
            InitializeComponent();

            MyDependencyService = new DependencyInjectionService();
            var navigationPage = new NavigationPage();

            RegisterDependencies(MyDependencyService, navigationPage.Navigation, remoteInstallers);
            var navigationService = MyDependencyService.Resolve<INavigationService>();
            navigationService.PushAsync<SummaryViewModel>();

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
        }

        protected override void OnResume()
        {
            MyDependencyService.Resolve<ITimerService>().ResumeTimer();
        }
    }
}
