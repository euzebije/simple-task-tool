using System;
using System.Collections.Generic;
using STT.Model.Entity;

namespace STT.Data
{
    public interface IRepositoryBase<TModel> where TModel: EntityBase
    {
        IEnumerable<TModel> Get();
        TModel Find(Guid key);

        void Save(TModel model);
        void Delete(TModel model);
    }
}
