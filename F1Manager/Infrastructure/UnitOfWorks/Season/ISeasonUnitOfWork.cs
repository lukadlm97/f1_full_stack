using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Season
{
    public interface ISeasonUnitOfWork
    {
        Domain.Season.ISeasonRepository SeasonRepository { get; set; }

        Task<int> Commit();
    }
}