using System.ComponentModel;
using System.Runtime.CompilerServices;
using MoveUp.Models;
using System.Collections.ObjectModel;

namespace MoveUp.ViewModels
{
    public class NewPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<HikingSavedData> Collection { get; set; } = new ObservableCollection<HikingSavedData>();

        public NewPageViewModel()
        {
            Collection.Add(new HikingSavedData());
            Collection.Add(new HikingSavedData());
            Collection.Add(new HikingSavedData());
            Collection.Add(new HikingSavedData());
            Collection.Add(new HikingSavedData());
            Collection.Add(new HikingSavedData());
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

