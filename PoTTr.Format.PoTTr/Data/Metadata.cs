using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace PoTTr.Format.PoTTr.Data
{
    [DataContract]
    public class Metadata
    {
        [DataMember(Order = 3, EmitDefaultValue = false)]
        public string? Copyright { get; set; }

        [DataMember(Order = 2, EmitDefaultValue = false)]
        public string? Description { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false)]
        public string? Title { get; set; }

        [DataMember(Order = 4, EmitDefaultValue = false)]
        [ProtoMember(4, AsReference = true)]
        public List<Agent> Agents { get; } = new List<Agent>();
    }
}
