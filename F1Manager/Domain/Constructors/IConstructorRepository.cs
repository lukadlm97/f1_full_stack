using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Constructors
{
    public interface IConstructorRepository : IRepository<Constructor>
    {
        Task<IEnumerable<Constructor>> GetConstructors(int countryId);

        Task<bool> RetireConstructor(int constructorId);

        Task<bool> MakeComeback(int constructorId);

        Task<bool> ChangeTeamNationality(int constructorId, int teamNationalityId);
    }
}