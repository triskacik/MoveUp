﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MoveUp.Services.Interfaces;
using System;

namespace MoveUp.Services
{
    public class DependencyInjectionService : IDependencyInjectionService
    {
        private IContainer container;

        public void Build(IServiceCollection serviceCollection)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(serviceCollection);
            container = containerBuilder.Build();
        }

        public TService Resolve<TService>()
        {
            return container.Resolve<TService>();
        }

        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }
    }
}