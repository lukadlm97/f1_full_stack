using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.TechnicalStuff
{
    public interface ITechnicalStaffUnitOfWork
    {
        Domain.TechnicalStaff.ITechnicalStaffRepository TechnicalStaffRepository { get; set; }

        Task<int> Commit();
    }
}