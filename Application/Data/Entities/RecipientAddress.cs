using System.Collections.Generic;
using Application.Common;
using Application.Data.Common;

namespace Application.Data.Entities
{
    public class RecipientAddress : VersionableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}