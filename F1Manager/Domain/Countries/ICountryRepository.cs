using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Countries
{
    public interface ICountryRepository : IRepository<Country>
    {
    }
}
