using System;
using System.ComponentModel;

namespace PaystubJsonApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (o, e) =>
        {
            Debug.Debug.Instance.Post("Event", $"{e.GetType()} - {e.PropertyName}");
            Console.WriteLine($"Object: {o}");
            Console.WriteLine($"Property Name: {e.PropertyName}");
        };

        public void NotifyOfPropertyChange( string propertyName )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
