using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constructors
{
    public interface IConstructorRepository: IRepository<Constructor>
    {
        Task<IEnumerable<Constructor>> GetConstructors(int countryId);
    }
}
