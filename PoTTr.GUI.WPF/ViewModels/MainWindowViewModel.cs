/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using PoTTr.Format.PoTTr.Data;
using Prism.Commands;
using Prism.Mvvm;

namespace PoTTr.GUI.WPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            NewProject = new DelegateCommand(async () => await newProject())!;
            SaveProject = new DelegateCommand(async () => await saveProject());
            SaveProjectAs = new DelegateCommand(async () => await saveProjectAs());
            OpenProject = new DelegateCommand(async () => await openProject());
            // UpdateProject = new DelegateCommand(async (TranscriptProject? context) => await updateContext(context))!;
            //this.WhenAnyValue(x => x.CurrentProject).InvokeCommand(UpdateProject);
        }

        private string? _testStr;
        public string? TestString { get => _testStr; set => SetProperty(ref _testStr, value); }

        private string? _location;
        public string? ProjectLocation { get => _location; set => SetProperty(ref _location, value); }

        private FileStream? _projectStream { get; set; }
        private SerializeFormat? _projectFormat { get; set; }


        private TranscriptProject? _currentProject;
        public TranscriptProject? CurrentProject
        {
            get => _currentProject; set
            {
                SetProperty(ref _currentProject, value);
                ContextLoaded = value != null;
                if (value?.Metadata?.Agents != null)
                    Agents = new ObservableCollection<AgentRowViewModel>(value.Metadata.Agents.Select(a => new AgentRowViewModel(a)));
            }
        }

        public DelegateCommand NewProject { get; }
        private async Task newProject()
        {
            //TODO: Ask to save
            await Task.Run(() => CurrentProject = TranscriptProject.DefaultProject);
        }

        public DelegateCommand SaveProjectAs { get; }
        private async Task saveProjectAs()
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "PoTTr Binary Project (*.pttr)|*.pttr|PoTTr Projects XML FIle (*.pttrx)|*.pttrx|PoTTr JSON Project (*.pttrj)|*.pttrj"
            };
            if (dialog.ShowDialog() ?? false)
            {
                _projectFormat = dialog.FilterIndex switch
                {
                    1 => SerializeFormat.Protobuf,
                    2 => SerializeFormat.Xml,
                    3 => SerializeFormat.Json,
                    _ => throw new ArgumentException("Invalid file format")
                };
                ProjectLocation = dialog.FileName;
            }
            else return;
            await Task.Run(_saveProject);
        }

        public DelegateCommand SaveProject { get; }
        private async Task saveProject()
        {
            if (_projectFormat == null || ProjectLocation == null)
                await saveProjectAs();
            else
                await Task.Run(_saveProject);
        }

        private void _saveProject()
        {
            if (_projectFormat == null || ProjectLocation == null || CurrentProject == null)
                throw new ArgumentNullException(nameof(CurrentProject));
            _projectStream?.Close();
            _projectStream = new FileStream(ProjectLocation, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            CurrentProject.SaveProject(_projectStream, _projectFormat.Value);
        }

        public DelegateCommand OpenProject { get; }
        private async Task openProject()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "PoTTr Projects (*.pttr, *.pttrj, *.pttrx)|*.pttr;*.pttrj;*.pttrx|PoTTr Binary Project (*.pttr)|*.pttr|PoTTr Projects XML FIle (*.pttrx)|*.pttrx|PoTTr JSON Project (*.pttrj)|*.pttrj"
            };
            if (dialog.ShowDialog() ?? false)
            {
                if (!File.Exists(dialog.FileName))
                    _ = MessageBox.Show($"Cannot open {dialog.FileName}. Please try again.");
                try
                {
                    _projectStream?.Close();
                    _projectStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                    {
                        await Task.Run(() =>
                        {
                            switch (dialog.FilterIndex)
                            {
                                case 1:
                                    switch (Path.GetExtension(dialog.FileName))
                                    {
                                        case "pttr":
                                            _tryOpen(_projectStream, SerializeFormat.Protobuf, dialog.FileName);
                                            break;
                                        case "pttrx":
                                            _tryOpen(_projectStream, SerializeFormat.Xml, dialog.FileName);
                                            break;
                                        case "prrtj":
                                            _tryOpen(_projectStream, SerializeFormat.Json, dialog.FileName);
                                            break;
                                        default:
                                            if (!_tryOpen(_projectStream, SerializeFormat.Protobuf, dialog.FileName))
                                                if (!_tryOpen(_projectStream, SerializeFormat.Protobuf, dialog.FileName))
                                                    if (!_tryOpen(_projectStream, SerializeFormat.Protobuf, dialog.FileName))
                                                        throw new ArgumentException();
                                            break;
                                    }
                                    break;
                                case 2:
                                    _tryOpen(_projectStream, SerializeFormat.Protobuf, dialog.FileName);
                                    break;
                                case 3:
                                    _tryOpen(_projectStream, SerializeFormat.Xml, dialog.FileName);
                                    break;
                                case 4:
                                    _tryOpen(_projectStream, SerializeFormat.Json, dialog.FileName);
                                    break;
                                default: throw new NotImplementedException();
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show($"Error opening {dialog.FileName}. {ex.Message}");
                }
                finally
                {
                    CurrentProject = null;
                    _projectStream?.Close();
                }

            }
        }

        private bool _tryOpen(FileStream stream, SerializeFormat format, string fileName)
        {
            TranscriptProject proj;
            if (TranscriptProject.TryOpen(stream, format, out proj))
            {
                _projectStream = stream;
                _projectFormat = format;
                ProjectLocation = fileName;

                return (CurrentProject = proj) != null;
            }
            else
                return false;

        }

        private ObservableCollection<AgentRowViewModel>? _agents;
        public ObservableCollection<AgentRowViewModel>? Agents { get => _agents; set => SetProperty(ref _agents, value); }

        private bool _contextLoaded = false;
        public bool ContextLoaded { get => _contextLoaded; private set => SetProperty(ref _contextLoaded, value); }

    }
}
