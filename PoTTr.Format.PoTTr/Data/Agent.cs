/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PoTTr.Format.PoTTr.Data
{
    public class Agent : IDataTable
    {
        public AgentType AgentType { get; set; }
        public List<Name> Names { get; } = new List<Name>();

        public int Id { get; set; }

        public List<ContentNode> ContentNodes { get; } = new List<ContentNode>();
        
        public IEnumerable<Agent> Actors { get => throw new NotImplementedException(); }
        public IEnumerable<Agent> Characters { get => throw new NotImplementedException(); }

        public int MetadataId { get; set; } 
        public Metadata Metadata { get; set; } = null!;

        public string? PickName
        {
            get => Names.First()?.Value;
        }
    }

    public enum AgentType { Person, Character, Group, Organization, Other }
}