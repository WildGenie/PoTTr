using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace PoTTr.Format.PoTTr.Data
{
    [DataContract]
    public class ProjectData
    {
        [DataMember(Order = 1, IsRequired = true)]
        public string DataKey { get; set; } = null!;

        [DataMember(Order = 2, IsRequired = true)]
        public string DataValue { get; set; } = null!;
}
}
