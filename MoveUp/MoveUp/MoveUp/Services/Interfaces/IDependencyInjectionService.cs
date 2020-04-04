using Microsoft.Extensions.DependencyInjection;
using System;

namespace MoveUp.Services.Interfaces
{
    public interface IDependencyInjectionService
    {
        void Build(IServiceCollection serviceCollection);
        TService Resolve<TService>();
        object Resolve(Type type);
    }
}