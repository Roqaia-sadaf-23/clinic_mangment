using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Domain.Common
{
    public abstract class Entity
    {
        private readonly List<DominEvent> _domainEvents = new();

        public int Id { get; private set; }   // 👈 مهم جدًا

        protected Entity() { }

        protected Entity(int id)
        {
            Id = id;
        }

        public void AddDomainEvent(DominEvent eventItem) => _domainEvents.Add(eventItem);

        public void RemoveDomainEvent(DominEvent eventItem) => _domainEvents.Remove(eventItem);

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}