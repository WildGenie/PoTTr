using System;
using System.Collections.Generic;
using System.Text;

namespace PoTTr.Format.PoTTr.Data
{
    public class AgentActor : IDataTable
    {
        public int Id { get; set; }

        public int CharacterId { get; set; }
        public Agent Character { get; set; } = null!;

        public int ActorId { get; set; }
        public Agent Actor { get; set; } = null!;
    }
}
