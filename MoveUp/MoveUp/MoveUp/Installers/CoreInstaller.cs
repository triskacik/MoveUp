using Microsoft.Extensions.DependencyInjection;
using MoveUp.Factories.Interfaces;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;
using MoveUp.Views;
using Xamarin.Forms;

namespace MoveUp.Installers
{
    public class CoreInstaller
    {
        public void Install(IServiceCollection serviceCollection, IDependencyInjectionService dependencyInjectionService,
            INavigation navigation)
        {
            serviceCollection.AddSingleton<IDependencyInjectionService>(dependencyInjectionService);
            serviceCollection.AddSingleton<INavigation>(navigation);

            serviceCollection.Scan(scan => scan
                .FromAssemblyOf<CoreInstaller>()
                .AddClasses(classes => classes.AssignableTo<IFactory>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingletonService>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime()
                .AddClasses(classes => classes.AssignableTo<ITransientService>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<IViewModel>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime()

                .AddClasses(classes => classes.AssignableTo<ViewBase>())
                    .AsSelf()
                    .WithTransientLifetime()
            );
        }
    }
}
