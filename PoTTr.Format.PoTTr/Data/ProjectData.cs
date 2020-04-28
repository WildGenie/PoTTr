using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PoTTr.Format.PoTTr.Data
{
    public class ProjectData
    {
        [MaxLength(50), Key]
        public string DataKey { get; set; } = null!;
        public string DataValue { get; set; } = null!;
}
}
