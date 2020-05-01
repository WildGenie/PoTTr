/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace PoTTr.Format.PoTTr.Data
{
    [DataContract]
    public class TranscriptProject : PoTTrSerializable<TranscriptProject>
    {
        public static TranscriptProject DefaultProject { get => new TranscriptProject(); }
        [DataMember(Order = 2, EmitDefaultValue = false)]
        public Metadata? Metadata { get; set; }

        [DataMember(Order = 6, EmitDefaultValue = false)]
        public Dictionary<string, string> ProjectData { get; } = new Dictionary<string, string>();

        [DataMember(Order = 1, EmitDefaultValue = false)]
        public List<ContentNode> Nodes { get; set; } = new List<ContentNode>();
    }
}
