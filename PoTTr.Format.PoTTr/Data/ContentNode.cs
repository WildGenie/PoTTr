using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PoTTr.Format.PoTTr.Data
{
    public class ContentNode : IMetadata, IDataTable, ITimeable
    {
        public int Id { get; set; }
        public int MetadataId { get; set; }
        public Metadata? Metadata { get; set; }
        public TimeSpan? Begin { get; set; }
        public TimeSpan? End { get; set; }

        public NodeType NodeType { get; set; }

        public string? Value { get; set; }

        public uint Order { get; set; }

        public List<ContentNode> ChildNodes { get; } = new List<ContentNode>();

        public int? ParentId { get; set; }
        public ContentNode? Parent { get; set; }
    }

    public enum NodeType { Division, Paragraph, Span, Break, Text, Root }
}
