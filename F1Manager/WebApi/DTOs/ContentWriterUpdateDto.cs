using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class ContentWriterUpdateDto:UserDto
    {
        public string AdminName { get; set; }

        public int CountryId { get; set; } = 0;
    }
}
