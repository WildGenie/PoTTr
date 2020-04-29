/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PoTTr.Format.PoTTr.Data
{
    [DataContract]
    public class TranscriptProject
    {
        [DataMember(Order = 2)]
        public Metadata? Metadata { get; set; }

        [DataMember(Order = 6)]
        public Dictionary<string, string> ProjectData { get; } = new Dictionary<string,string>();

        [DataMember(Order = 1)]
        public List<ContentNode> Nodes { get; } = new List<ContentNode>();
    }
}
