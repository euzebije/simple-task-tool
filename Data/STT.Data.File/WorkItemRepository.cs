using System.IO;
using STT.Model.Entity;

namespace STT.Data.File
{
    internal class WorkItemRepository : BaseRepository<WorkItem>, IWorkItemRepository
    {
        private readonly string _filePath;
        protected override string FilePath
        {
            get { return _filePath; }
        }

        public WorkItemRepository(string dataFolder)
        {
            _filePath = Path.Combine(dataFolder, "WorkItem.stt");
            if (!System.IO.File.Exists(_filePath))
            {
                var stream = System.IO.File.Create(_filePath);
                stream.Dispose();
            }
        }
    }
}
