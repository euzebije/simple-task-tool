using System;
using System.Collections.Generic;
using STT.Model.Entity;

namespace STT.Data
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        IEnumerable<Project> GetWithWorkItems();
        Project FindWithWorkItems(Guid key);
    }
}
