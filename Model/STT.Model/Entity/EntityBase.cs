using System;

namespace STT.Model.Entity
{
    public abstract class EntityBase
    {
        public Guid Key { get; set; }
        public bool IsArchived { get; set; }

        protected EntityBase()
        {
            Key = Guid.NewGuid();
        }

        public bool IsEqualTo(EntityBase entity)
        {
            if (entity == null)
                return false;

            return Key == entity.Key;
        }
    }
}
