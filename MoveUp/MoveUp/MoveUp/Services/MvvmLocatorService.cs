﻿using MoveUp.Services.Interfaces;
using MoveUp.Views;
using System;
using Xamarin.Forms;

namespace MoveUp.Services
{
    public class MvvmLocatorService : IMvvmLocatorService
    {
        private readonly IDependencyInjectionService dependencyInjectionService;

        public MvvmLocatorService(IDependencyInjectionService dependencyInjectionService)
        {
            this.dependencyInjectionService = dependencyInjectionService;
        }

        public Page ResolveView<TViewModel>(TViewModel viewModel = default)
        {
            var viewType = GetViewType(viewModel);
            return GetView(viewType);
        }

        private Type GetViewType<TViewModel>(TViewModel viewModel)
        {
            var viewModelType = viewModel?.GetType() ?? typeof(TViewModel);
            var viewTypeName = viewModelType
                .AssemblyQualifiedName
                .Replace(viewModelType.Assembly.GetName().Name, typeof(ViewBase).Assembly.GetName().Name)
                .Replace("ViewModel", "View");

            var viewType = Type.GetType(viewTypeName);
            if (viewType != null)
            {
                return viewType;
            }
            else
            {
                throw new Exception();
            }
        }

        private Page GetView(Type viewType)
        {
            return dependencyInjectionService.Resolve(viewType) as Page;
        }
    }
}