using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace PoTTr.Format.PoTTr.Data
{
    [DataContract]
    public class ContentNode
    {
        [DataMember(Order = 6, EmitDefaultValue = false)]
        public Metadata? Metadata { get; set; }

        [DataMember(Order = 3, EmitDefaultValue = false)]
        public TimeSpan? Begin { get; set; }

        [DataMember(Order = 4, EmitDefaultValue = false)]
        public TimeSpan? End { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false)]
        public NodeType Type { get; set; }

        [DataMember(Order = 2, EmitDefaultValue = false)]
        public string? Value { get; set; }

        // May not need this?
        public uint Order { get; set; }


        [DataMember(Order = 5, EmitDefaultValue = false)]
        public List<ContentNode> ChildNodes { get; } = new List<ContentNode>();

        [DataMember(Order = 7, EmitDefaultValue = false)]
        [ProtoMember(7, AsReference = true)]
        public Agent? Agent { get; set; }
    }

    public enum NodeType { Division, Paragraph, Span, Break, Text, Root }
}
