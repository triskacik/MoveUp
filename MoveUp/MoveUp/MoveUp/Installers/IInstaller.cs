using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoveUp.Installers
{
    public interface IInstaller
    {
        void Install(IServiceCollection serviceCollection);
    }
}
