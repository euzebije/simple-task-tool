using System;
using System.Collections.Generic;
using STT.Model.Entity;

namespace STT.Data.Memory
{
    internal class WorkItemRepository : BaseRepository<WorkItem>, IWorkItemRepository
    {
        public override void Save(WorkItem model)
        {
            base.Save(model);
            foreach (var child in model.Children)
            {
                if (!Items.Contains(child))
                    Items.Add(child);
            }
        }

        public override void Delete(WorkItem model)
        {
            base.Delete(model);
            foreach (var child in model.Children)
            {
                if (Items.Contains(child))
                    Items.Remove(child);
            }
        }

        public IEnumerable<WorkItem> GetWithChildren()
        {
            return Get();
        }

        public WorkItem FindWithChildren(Guid key)
        {
            return Find(key);
        }
    }
}
