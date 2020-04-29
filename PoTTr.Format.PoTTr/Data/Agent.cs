/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using ProtoBuf;

namespace PoTTr.Format.PoTTr.Data
{
    [ProtoContract]
    [DataContract]
    public class Agent
    {
        [DataMember(Order = 1, IsRequired = true)]
        public AgentType Type { get; set; }

        [DataMember(Order = 2, IsRequired = true)]
        public List<Name> Names { get; } = new List<Name>();

        [DataMember(Order = 3, IsRequired = false)]
        [ProtoMember(3, AsReference = true, IsRequired = false)]
        public List<Agent> Actor { get; } = new List<Agent>();

    }

    public enum AgentType { Person = 1, Character, Group, Organization, Other }
}