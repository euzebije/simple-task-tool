using System;
using System.Collections.Generic;
using System.Linq;
using STT.Model.Entity;

namespace STT.Data.Memory
{
    internal abstract class BaseRepository<TModel> : IRepositoryBase<TModel> where TModel: EntityBase
    {
        protected HashSet<TModel> Items { get; private set; }

        protected BaseRepository()
        {
            Items = new HashSet<TModel>();
        }

        public virtual IEnumerable<TModel> Get()
        {
            return Items.Where(x => !x.IsArchived);
        }

        public virtual TModel Find(Guid key)
        {
            return Get().SingleOrDefault(x => x.Key == key);
        }

        public virtual void Save(TModel model)
        {
            if (!Items.Contains(model))
                Items.Add(model);
        }

        public virtual void Delete(TModel model)
        {
            if (Items.Contains(model))
                Items.Remove(model);
        }
    }
}
