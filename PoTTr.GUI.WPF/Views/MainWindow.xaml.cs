/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Windows;
using PoTTr.GUI.WPF.ViewModels;

namespace PoTTr.GUI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel() { }; /*

            this.WhenActivated(dr =>
              {
                  this.BindCommand(ViewModel, vm => vm.NewProject, v => v.MenuNew).DisposeWith(dr);

                  this.BindCommand(ViewModel, vm => vm.OpenProject, v => v.MenuOpen).DisposeWith(dr);

                  this.BindCommand(ViewModel, vm => vm.SaveProject, v => v.MenuSave).DisposeWith(dr);
                  this.OneWayBind(ViewModel, vm => vm.ContextLoaded, v => v.MenuSave.IsEnabled).DisposeWith(dr);

                  //this.OneWayBind(ViewModel, vm => vm.ProjectLocation, v => v.WindowTitle).DisposeWith(dr);

                  this.BindCommand(ViewModel, vm => vm.SaveProjectAs, v => v.MenuSaveAs).DisposeWith(dr);
                  this.OneWayBind(ViewModel, vm => vm.ContextLoaded, v => v.MenuSaveAs.IsEnabled).DisposeWith(dr);

                  //this.OneWayBind(ViewModel, vm => vm.Agents, v => v.AgentStack.ItemsSource);

              });*/

        }

        string _windowTitle = "";
        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                Dispatcher.Invoke(() => Title = "PoTTr" + (string.IsNullOrEmpty(value) ? "" : " - " + value ) ) ;
                _windowTitle = value;
            }
        }
    }
}
