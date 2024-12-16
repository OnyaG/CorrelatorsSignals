using CorrelatorsSignals.ViewModels.Tables;
using System;
using System.Windows.Input;

namespace CorrelatorsSignals.ViewModels
{
    public class MainViewModel : NotifyPropertyChanged
    {
        public CorrelatorsTableViewModel CorrelatorsTableViewModel { get; set; }

        public MainViewModel()
        {

            CorrelatorsTableViewModel = new CorrelatorsTableViewModel();

            AddCommand = new ActionCommand(o =>
            {
                RunCommand(CorrelatorsTableViewModel.Create);
            });

            RemoveCommand = new ActionCommand(o =>
            {

                RunCommand(CorrelatorsTableViewModel.Delete);
            });

            ApplyCommand = new ActionCommand(o =>
            {
                RunCommand(CorrelatorsTableViewModel.Save);
            });

            RefreshCommand = new ActionCommand(o =>
            {
                RunCommand(CorrelatorsTableViewModel.Refresh);
            });
        }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand ApplyCommand { get; }
        public ICommand RefreshCommand { get; }

        

        private void RunCommand(Action commandImpl)
        {
            commandImpl();
        }
    }
}
