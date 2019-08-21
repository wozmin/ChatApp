using ChatServer.EF;
using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _userRepo;
        private IChatRepository _chatRepo;
        private ApplicationContext _db { get; set; }


        public UnitOfWork(ApplicationContext db)
        {
            _db = db;
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new UserRepository(_db);
                }
                return _userRepo;
            }
        }

        public IChatRepository Chats
        {
            get
            {
                if (_chatRepo == null)
                {
                    _chatRepo = new ChatRepository(_db);
                }
                return _chatRepo;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
