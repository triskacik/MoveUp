using MoveUp.ViewModels;

namespace MoveUp.Views
{
    public partial class SummaryView
    {
        private SummaryViewModel vm;

        public SummaryView(SummaryViewModel summaryViewModel)
        {
            BindingContext = summaryViewModel;
            vm = summaryViewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.Refresh();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.SavePolyline();
        }
    }
}