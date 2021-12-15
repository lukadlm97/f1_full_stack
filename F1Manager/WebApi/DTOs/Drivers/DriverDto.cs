using System;

namespace WebApi.DTOs.Drivers
{
    public class DriverDto
    {
        public int KeggleId { get; set; } = -1;
        public string DriverRef { get; set; }
        public int? Number { get; set; }
        public string Code { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string WikiUrl { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsRetired { get; set; } = false;
    }
}