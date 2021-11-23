using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Drivers
{
    public partial class Driver
    {
        public Driver(string forename,string surname,int countryId,string driverRef)
        {
            Forename = forename;
            Surname = surname;
            CountryId = countryId;
            DriverRef = driverRef;
        }

        public bool ValidOnAdd()
        {
            return
                // Validate forename
                !string.IsNullOrEmpty(Forename)
                // Make sure Surname not null 
                && !string.IsNullOrEmpty(Surname)
                && !string.IsNullOrEmpty(DriverRef)
                // Make sure Country is not null
                && (
                    Role != null
                    || RoleId != 0
                );
        }
    }
}
