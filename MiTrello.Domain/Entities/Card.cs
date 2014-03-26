using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrello.Domain.Entities
{
    public class Cards : IEntity
    {
        public virtual string Information { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
    }
}