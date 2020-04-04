using Xamarin.Forms;
using MoveUp.Services;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using MoveUp.Installers;
using MoveUp.Views;

namespace MoveUp
{
    public partial class App : Application
    {
        public IDependencyInjectionService MyDependencyService { get; }

        public App()
        {
            InitializeComponent();

            MyDependencyService = new DependencyInjectionService();
            var navigationPage = new NavigationPage();

            RegisterDependencies(MyDependencyService, navigationPage.Navigation);
            var navigationService = MyDependencyService.Resolve<INavigationService>();
            navigationService.PushAsync<SummaryViewModel>();

            MainPage = navigationPage;
        }

        private void RegisterDependencies(IDependencyInjectionService dependencyService, INavigation navigation)
        {
            var serviceCollection = new ServiceCollection();
            var coreInstaller = new CoreInstaller();
            coreInstaller.Install(serviceCollection, dependencyService, navigation);

            dependencyService.Build(serviceCollection);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
