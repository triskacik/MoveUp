using Microsoft.Extensions.DependencyInjection;
using MoveUp.Factories.Interfaces;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;
using MoveUp.Views;
using Xamarin.Forms;
using MoveUp.Installers;

namespace MoveUp.iOS.Installers
{
    public class iOSInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan
                .FromAssemblyOf<iOSInstaller>()
                .AddClasses(classes => classes.AssignableTo<ISingletonService>())
                    .AsSelfWithInterfaces()
                    .WithSingletonLifetime()
            );
        }
    }
}
