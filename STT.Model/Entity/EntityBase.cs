using System;

namespace STT.Model.Entity
{
    public abstract class EntityBase
    {
        public abstract Guid Key { get; set; }
        public bool IsArchived { get; set; }
    }
}
