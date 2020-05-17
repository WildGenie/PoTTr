/* Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System.Linq;
using PoTTr.Format.PoTTr.Data;
using Prism.Mvvm;

namespace PoTTr.GUI.WPF.ViewModels
{
    public class AgentRowViewModel: BindableBase
    {
        public Agent Agent { get; }
        public AgentRowViewModel()  => Agent = new Agent();
        public AgentRowViewModel(Agent agent) => Agent = agent;
    }
}
