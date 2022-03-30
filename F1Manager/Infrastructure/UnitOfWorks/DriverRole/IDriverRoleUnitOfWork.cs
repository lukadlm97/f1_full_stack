using Domain.DriverRoles;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.DriverRole
{
    public interface IDriverRoleUnitOfWork
    {
        IDriverRolesRepository DriverRolesRepository { get; set; }

        Task<int> Commit();
    }
}