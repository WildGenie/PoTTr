using System;
using System.Collections.Generic;
using System.Text;

namespace PoTTr.Format.PoTTr.Data
{
    public class Metadata : IDataTable
    {
        public List<Agent> Agents { get; } = new List<Agent>();
        public string? Copyright { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }

        public int Id { get; set; }
    }
}
