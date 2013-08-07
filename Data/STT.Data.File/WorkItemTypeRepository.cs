using System.IO;
using STT.Model.Entity;

namespace STT.Data.File
{
    internal class WorkItemTypeRepository : BaseRepository<WorkItemType>, IWorkItemTypeRepository
    {
        private readonly string _filePath;
        protected override string FilePath
        {
            get { return _filePath; }
        }

        public WorkItemTypeRepository(string dataFolder)
        {
            _filePath = Path.Combine(dataFolder, "WorkItemType.stt");
            if (!System.IO.File.Exists(_filePath))
            {
                var stream = System.IO.File.Create(_filePath);
                stream.Dispose();
            }
        }
    }
}
