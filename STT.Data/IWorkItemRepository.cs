using System;
using System.Collections.Generic;
using STT.Model.Entity;

namespace STT.Data
{
    public interface IWorkItemRepository : IRepositoryBase<WorkItem>
    {
        WorkItem FindWithChildren(Guid key);
        IEnumerable<WorkItem> GetWithChildren();
    }
}
