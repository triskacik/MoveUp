using System;
using Xamarin.Forms;
using MoveUp.Views;
using Xamarin.Forms.Xaml;

namespace MoveUp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SummaryView();
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
