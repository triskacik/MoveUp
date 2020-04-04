using MoveUp.ViewModels;

namespace MoveUp.Views
{
    public partial class SummaryView
    {
        public SummaryView(SummaryViewModel summaryViewModel)
        {
            BindingContext = summaryViewModel;
            InitializeComponent();
        }
    }
}