using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrello.Domain.Entities
{
    public class Lane : IEntity
    {
        private readonly IList<Cards> _cards = new List<Cards>();
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual IEnumerable<Cards> Cards { get { return _cards; } }

        public virtual void AddCard(Cards card)
        {
            if (!_cards.Contains(card))
            {
                _cards.Add(card);
            }
        }

        public virtual void RemoveCard(Cards card)
        {

            _cards.Remove(card);

        }
    }
}