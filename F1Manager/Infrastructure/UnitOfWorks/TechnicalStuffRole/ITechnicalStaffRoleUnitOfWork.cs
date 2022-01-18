using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.TechnicalStuffRole
{
    public interface ITechnicalStaffRoleUnitOfWork
    {
        Domain.TechnicalStaffRole.ITechnicalStaffRoleRepository TechnicalStaffRole { get; set; }

        Task<int> Commit();
    }
}