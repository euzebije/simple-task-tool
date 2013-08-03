using System;
using System.Collections.Generic;
using STT.Model.Entity;

namespace STT.Data
{
    public interface IWorkItemRepository : IRepositoryBase<WorkItem>
    {
        IEnumerable<WorkItem> GetWithChildren();
        WorkItem FindWithChildren(Guid key);
    }
}
