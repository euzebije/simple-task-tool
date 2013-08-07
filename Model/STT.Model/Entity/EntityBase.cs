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

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            var entity = obj as EntityBase;
            if (entity != null)
            {
                return GetHashCode() == entity.GetHashCode();
            }

            return false;
        }
    }
}
