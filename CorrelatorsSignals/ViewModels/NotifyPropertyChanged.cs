using System.ComponentModel;

namespace CorrelatorsSignals.ViewModels
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate
        {
        };

        public void SetProperty<T>(ref T x, T value, string name)
        {
            if ((x == null || !x.Equals(value)) && (x != null || value != null))
            {
                x = value;
                RaisePropertyChanged(name);
            }
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
