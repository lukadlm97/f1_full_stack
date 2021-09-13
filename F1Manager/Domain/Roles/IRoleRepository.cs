using Domain.Interfaces;
using System.Threading.Tasks;

namespace Domain.Roles
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> AddRole(string roleName);
    }
}