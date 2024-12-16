using CorrelatorsSignals.ViewModels;
using System.Windows;

namespace CorrelatorsSignals.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new MainViewModel();
            this.DataContext = vm;
        }
    }
}
