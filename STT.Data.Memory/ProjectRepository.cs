﻿using System;
using System.Collections.Generic;
using STT.Model.Entity;

namespace STT.Data.Memory
{
    internal class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public Project FindWithWorkItems(Guid key)
        {
            return Find(key);
        }

        public IEnumerable<Project> GetWithWorkItems()
        {
            return Get();
        }
    }
}
