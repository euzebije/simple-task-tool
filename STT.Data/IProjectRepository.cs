using System;
using System.Collections.Generic;
using STT.Model.Entity;

namespace STT.Data
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        Project FindWithWorkItems(Guid key);
        IEnumerable<Project> GetWithWorkItems();
    }
}
