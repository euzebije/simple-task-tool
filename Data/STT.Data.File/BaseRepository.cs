using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using STT.Model.Entity;

namespace STT.Data.File
{
    internal abstract class BaseRepository<TModel> : IRepositoryBase<TModel> where TModel: EntityBase
    {
        protected abstract string FilePath { get; }

        protected HashSet<TModel> Load()
        {
            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    var items = JsonConvert.DeserializeObject<HashSet<TModel>>(reader.ReadToEnd());
                    return items ?? new HashSet<TModel>();
                }
            }
        }
        protected void Save(HashSet<TModel> items)
        {
            using (var stream = new FileStream(FilePath, FileMode.Truncate, FileAccess.Write))
            {
                using (var writer = new StreamWriter(stream))
                {
                    var data = JsonConvert.SerializeObject(items, Formatting.None,
                                                           new JsonSerializerSettings
                                                               {
                                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                               });
                    writer.Write(data);
                }
            }
        }

        public virtual IEnumerable<TModel> Get()
        {
            return Load().Where(x => !x.IsArchived);
        }

        public virtual TModel Find(Guid key)
        {
            return Get().SingleOrDefault(x => x.Key == key);
        }

        public virtual void Save(TModel model)
        {
            var items = Load();
            var item = items.SingleOrDefault(x => x.Key == model.Key);
            if (item != null)
            {
                items.Remove(item);
                items.Add(model);
            }
            else
            {
                items.Add(model);
            }
            Save(items);
        }

        public virtual void Delete(TModel model)
        {
            var items = Load();
            var item = items.SingleOrDefault(x => x.Key == model.Key);
            if (item != null)
            {
                items.Remove(item);
                Save(items);
            }
        }
    }
}
