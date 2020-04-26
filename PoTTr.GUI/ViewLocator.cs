// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using PoTTr.GUI.ViewModels;

namespace PoTTr.GUI
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl? Build(object data)
        {
            string name = data?.GetType()?.FullName?.Replace("ViewModel", "View") ?? string.Empty;
            Type? type = Type.GetType(name);

            return type != null ? Activator.CreateInstance(type!) as Control : new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}