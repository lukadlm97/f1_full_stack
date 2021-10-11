using Domain.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Countries
{
    public interface ICountriesUnitOfWork: IDisposable
    {
        ICountryRepository Countries { get; set; }

        Task<int> Commit();
    }
}
