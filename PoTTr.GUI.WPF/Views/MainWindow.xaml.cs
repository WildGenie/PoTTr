using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PoTTr.GUI.WPF.ViewModels;
using ReactiveUI;

namespace PoTTr.GUI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MainWindowViewBase
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainWindowViewModel() { };

            _ = this.WhenActivated(disposableRegistration =>
              {
                  this.BindCommand(ViewModel,
                      vm => vm.OpenProject,
                      v => v.OpenButton);
                  this.OneWayBind(ViewModel,
                      vm => vm.Agents,
                      v => AgentStack.ItemsSource);
              });

        }
    }

    public class MainWindowViewBase : ReactiveWindow<MainWindowViewModel> { /* No code needed here */ }
}
