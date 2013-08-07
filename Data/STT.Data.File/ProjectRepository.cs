using System;
using System.Collections.Generic;
using System.IO;
using STT.Model.Entity;

namespace STT.Data.File
{
    internal class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly IWorkItemRepository _workItemRepository;

        private readonly string _filePath;
        protected override string FilePath
        {
            get { return _filePath; }
        }

        public ProjectRepository(string dataFolder, IWorkItemRepository workItemRepository)
        {
            if (workItemRepository == null)
                throw new ArgumentNullException("workItemRepository");

            _workItemRepository = workItemRepository;

            _filePath = Path.Combine(dataFolder, "Project.stt");
            if (!System.IO.File.Exists(_filePath))
            {
                var stream = System.IO.File.Create(_filePath);
                stream.Dispose();
            }
        }

        public override void Save(Project model)
        {
            base.Save(model);

            foreach (var workItem in model.WorkItems)
            {
                _workItemRepository.Save(workItem);
            }
        }

        public IEnumerable<Project> GetWithWorkItems()
        {
            return Get();
        }

        public Project FindWithWorkItems(Guid key)
        {
            return Find(key);
        }
    }
}
