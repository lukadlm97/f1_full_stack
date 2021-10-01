using Domain.Users;
using System;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Users
{
    public interface IUsersUoW : IDisposable
    {
        IUserRepository Users { get; set; }

        Task<int> Commit();
    }
}