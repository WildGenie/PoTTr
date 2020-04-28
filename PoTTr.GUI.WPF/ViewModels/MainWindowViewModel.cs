using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Win32;
using PoTTr.Format.PoTTr;
using ReactiveUI;
using PoTTr.Format.PoTTr.Data;
using DynamicData.Binding;
using DynamicData;
using System.Collections.ObjectModel;

namespace PoTTr.GUI.WPF.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        public MainWindowViewModel()
        {
            NewProject = ReactiveCommand.CreateFromTask(newProject)!;
            OpenProject = ReactiveCommand.CreateFromTask(openProject)!;
            UpdateContext = ReactiveCommand.CreateFromTask(async (PoTTrContext? context) => updateContext(context))!;
            this.WhenAnyValue(x => x.DbContext).InvokeCommand(UpdateContext);
        }

        private string? _testStr;
        public string? TestString { get => _testStr; set => this.RaiseAndSetIfChanged(ref _testStr, value); }

        private PoTTrContext? _dbContext;
        public PoTTrContext? DbContext { get => _dbContext; set => this.RaiseAndSetIfChanged(ref _dbContext, value); }

        public ReactiveCommand<Unit, Unit> NewProject { get; }
        private async Task newProject()
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "PoTTr Projects (*.pttr)|*.pttr"
            };
            if (dialog.ShowDialog() ?? false)
            {
                try
                {
                    await OpenContext(dialog.FileName);
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show($"Error opening {dialog.FileName}. {ex.Message}");
                }

            }
        }

        public ReactiveCommand<PoTTrContext, Unit> UpdateContext { get; }
        private void updateContext(PoTTrContext? context)
        {
            Agents = context?.Agents?.Local?.ToObservableCollection()?.ToObservableChangeSet()?.AsObservableList();
        }


        public ReactiveCommand<Unit, Unit> OpenProject { get; }
        private async Task openProject()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "PoTTr Projects (*.pttr)|*.pttr"
            };
            if (dialog.ShowDialog() ?? false)
            {
                if (!File.Exists(dialog.FileName))
                    _ = MessageBox.Show($"Cannot open {dialog.FileName}. Please try again.");
                try
                {
                    await OpenContext(dialog.FileName);
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show($"Error opening {dialog.FileName}. {ex.Message}");
                }

            }
        }

        private async Task OpenContext(string filePath)
        {
            var optBuilder = new DbContextOptionsBuilder<PoTTrContext>().UseSqlite($"Data Source={filePath}");
            DbContext = new PoTTrContext(optBuilder.Options);
            bool migrationNeeded = (await DbContext.Database.GetPendingMigrationsAsync()).Any();
            if (migrationNeeded)
                DbContext.Database.Migrate();

            TestString = DbContext.Database.GetDbConnection().ConnectionString;
        }

        private ReadOnlyObservableCollection<Agent>? _agents;
        public ReadOnlyObservableCollection<Agent>? Agents { get => _agents; set => this.RaiseAndSetIfChanged(ref _agents, value); }

    }
}
