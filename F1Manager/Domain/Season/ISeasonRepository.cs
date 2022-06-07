using Domain.Interfaces;

namespace Domain.Season
{
    public interface ISeasonRepository : IRepository<Season>,ISingleGet<Season>
    {
    }
}