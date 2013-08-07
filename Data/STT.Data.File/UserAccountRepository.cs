using System.IO;
using STT.Model.Entity;

namespace STT.Data.File
{
    internal class UserAccountRepository : BaseRepository<UserAccount>, IUserAccountRepository
    {
        private readonly string _filePath;
        protected override string FilePath
        {
            get { return _filePath; }
        }

        public UserAccountRepository(string dataFolder)
        {
            _filePath = Path.Combine(dataFolder, "UserAccount.stt");
            if (!System.IO.File.Exists(_filePath))
            {
                var stream = System.IO.File.Create(_filePath);
                stream.Dispose();
            }
        }
    }
}
