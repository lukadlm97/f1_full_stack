using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class AssignCountryDto
    {
        public int CountryId { get; set; } = default;
        public string CountryName { get; set; } = default;
    }
}
