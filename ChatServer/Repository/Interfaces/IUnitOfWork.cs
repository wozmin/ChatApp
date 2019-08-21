using System.Threading.Tasks;

namespace ChatServer.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IChatRepository Chats { get; }
        Task SaveChangesAsync();
    }
}
