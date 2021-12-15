using Domain.Countries;
using Domain.Drivers;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Seed.CountrySeed
{
    public class SeedData
    {
        public static void SeedCountriesData(AppDbContext context)
        {
            if (!context.Countries.Any())
            {
                var countries = GetData();

                foreach (var country in countries)
                {
                    context.Countries.Add(country);
                }

                context.SaveChanges();
            }
        }

        public static void SetActiveDrivers(AppDbContext context)
        {
            if (context.Drivers.Any())
            {
                var drivers
                    = context.Drivers;

                foreach (var driver in  drivers)
                {
                    if (driver.Number != null)
                    {
                        driver.IsActive = true;
                    }
                }

                context.SaveChanges();
            }
        }

        public static void SetRetairDrivers(AppDbContext context)
        {
            if (context.Drivers.Any())
            {
                var drivers
                    = context.Drivers;

                foreach (var driver in drivers)
                {
                    if (driver.Number == null)
                    {
                        driver.IsRetired = true;
                    }
                }

                context.SaveChanges();
            }
        }

        public static async Task SeedDriverssData(AppDbContext context)
        {
            var drivers = GetDriverData();

            foreach (var driver in drivers)
            {
                driver.Country = await GetOrigin(driver.Country.Name,context);

                context.Add(driver);
            }

            context.SaveChanges();
            
        }

        public static List<Country> GetData()
        {
            List<Country> countries = new List<Country>();
            try
            {
                var _xl = new Microsoft.Office.Interop.Excel.Application();
                var wb = _xl.Workbooks.Open(@"C:\Users\luka.radovanovic\source\repos\f1_strategy_game\data-resources\GDPdata.xlsx");
                var sheets = wb.Sheets;
                DataSet dataSet = null;

                if (sheets != null && sheets.Count != 0)
                {
                    dataSet = new DataSet();
                    foreach (var item in sheets)
                    {
                        var sheet = (Microsoft.Office.Interop.Excel.Worksheet)item;
                        DataTable dt = null;

                        if (sheet != null)
                        {
                            dt = new DataTable();
                            var ColumnCount = ((Microsoft.Office.Interop.Excel.Range)sheet.UsedRange.Rows[1, Type.Missing]).Columns.Count;
                            var rowCount = ((Microsoft.Office.Interop.Excel.Range)sheet.UsedRange.Columns[1, Type.Missing]).Rows.Count;

                            for (int j = 0; j < ColumnCount; j++)
                            {
                                var cell = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, j + 1];
                                var column = new DataColumn(true ? (string)cell.Value : string.Empty);
                                dt.Columns.Add(column);
                            }

                            for (int i = 0; i < rowCount - 1; i++)
                            {
                                var r = dt.NewRow();
                                for (int j = 0; j < ColumnCount; j++)
                                {
                                    var cell = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[i + 1 + (true ? 1 : 0), j + 1];
                                    r[j] = cell.Value;
                                }
                                countries.Add(new Country
                                {
                                    KeggleId = Convert.ToInt32(r[0]),
                                    Name = (string)r[1],
                                    NominalGDP = Convert.ToDecimal(r[2]),
                                    Population = Convert.ToInt32(r[5]),
                                    GDPPerCapita = Convert.ToDecimal(r[6]),
                                    ShareIfWorldGDP = Convert.ToDecimal(r[7]),
                                    Code = ExstractCode(r[1].ToString())
                                  
                                });
                                if (countries.Count == 129)
                                {
                                    Console.WriteLine("");
                                }
                                Console.WriteLine(countries.Count);
                                //dt.Rows.Add(r);
                            }
                        }
                        dataSet.Tables.Add(dt);
                    }
                }

                return countries;
            }
            catch (Exception e)
            {
                return
                    null;
            }
            finally
            {
            }
        }

        public static List<Driver> GetDriverData()
        {
            List<Driver> drivers = new List<Driver>();
            try
            {
                var _xl = new Microsoft.Office.Interop.Excel.Application();
                var wb = _xl.Workbooks.Open(@"C:\Users\luka.radovanovic\source\repos\f1_strategy_game\data-resources\drivers.xlsx");
                var sheets = wb.Sheets;
                DataSet dataSet = null;

                if (sheets != null && sheets.Count != 0)
                {
                    dataSet = new DataSet();
                    foreach (var item in sheets)
                    {
                        var sheet = (Microsoft.Office.Interop.Excel.Worksheet)item;
                        DataTable dt = null;

                        if (sheet != null)
                        {
                            dt = new DataTable();
                            var ColumnCount = ((Microsoft.Office.Interop.Excel.Range)sheet.UsedRange.Rows[1, Type.Missing]).Columns.Count;
                            var rowCount = ((Microsoft.Office.Interop.Excel.Range)sheet.UsedRange.Columns[1, Type.Missing]).Rows.Count;

                            for (int j = 0; j < ColumnCount; j++)
                            {
                                var cell = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, j + 1];
                                var column = new DataColumn(true ? (string)cell.Value : string.Empty);
                                dt.Columns.Add(column);
                            }

                            for (int i = 0; i < rowCount - 1; i++)
                            {
                                var r = dt.NewRow();
                                for (int j = 0; j < ColumnCount; j++)
                                {
                                    var cell = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[i + 1 + (true ? 1 : 0), j + 1];
                                    r[j] = cell.Value;
                                }
                                drivers.Add(new Driver
                                {
                                    KeggleId = Convert.ToInt32(r[0]),
                                    DriverRef = (string)r[1],
                                    Number = GetDriverNumber(r[2]),
                                    Code = (string)r[3],
                                    Forename = (string)r[4],
                                    Surname = (string)r[5],
                                    DateOfBirth = GetDriverDoB(r[6]),
                                    WikiUrl = (string)r[8],
                                    Country =  new Country
                                    {
                                        Name=(string)r[7]
                                    }
                                });
                                Console.WriteLine(drivers.Count);
                                //dt.Rows.Add(r);
                            }
                        }
                        dataSet.Tables.Add(dt);
                    }
                }

                return drivers;
            }
            catch (Exception e)
            {
                throw e;
                return
                    null;
            }
            finally
            {
            }
        }

        public static string ExstractCode(string name)
        {
            char[] charsToTrim = { '*', ' ', '\'' };
            var result = name.Trim(charsToTrim);

            var spplited = result.Split(' ');

            if (spplited.Length != 1)
            {
                return new String(name.Split(' ').Select(x => x[0]).ToArray());
            }

            return result.ToUpper().Substring(0, 3);
        }

        private static int? GetDriverNumber(object rawNumber)
        {
            int number = 0;
            var numberAsString = (string)rawNumber;
            if(int.TryParse(numberAsString,out number))
            {
                return number;
            }
            return null;
        }
        private static DateTime GetDriverDoB(object rawDate)
        {
            DateTime dob = DateTime.MinValue;
            var dateAsString = (string)rawDate;

            if (DateTime.TryParse(dateAsString, out dob))
            {
                return dob;
            }
            return dob;
        }

        private static async Task<Country> GetOrigin(string nationality,AppDbContext dbContext)
        {
            char[] charsToTrim = { '*', ' ', '\'' };
            var result = nationality.Trim(charsToTrim);
            var country = await dbContext.Countries.FirstOrDefaultAsync();

            switch (result)
            {
                case "British": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("UK".ToLower()));break;
                case "German": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("GER".ToLower()));break;
                case "Spanish": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("SPA".ToLower()));break;
                case "Finnish": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("FIN".ToLower()));break;
                case "Japanese": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("JAP".ToLower()));break;
                case "French": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("FRA".ToLower()));break;
                case "Polish": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("POL".ToLower()));break;
                case "Brazilian": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("BRA".ToLower()));break;
                case "Italian": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("ITA".ToLower()));break;
                case "Australian": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("AUL".ToLower()));break;
                case "Austrian": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("AUS".ToLower()));break;
                case "American": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("US".ToLower()));break;
                case "Dutch": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("NET".ToLower()));break;
                case "Colombian": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("COL".ToLower()));break;
                case "Portuguese": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("POR".ToLower()));break;
                case "Canadian": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("CAN".ToLower()));break;
                case "Indian": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("IND".ToLower()));break;
                case "Hungarian": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("HUN".ToLower()));break;
                case "Irish": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("IRE".ToLower()));break;
                case "Danish": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("DEN".ToLower()));break;
                case "Argentine": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("ARG".ToLower()));break;
                case "Czech": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("CR(".ToLower()));break;
                case "Malaysian": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("MSY".ToLower()));break;
                case "Swiss": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("SWI".ToLower()));break;
                case "Belgian": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("BLG".ToLower()));break;
                case "Monegasque": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("MON".ToLower()));break;
                case "Swedish": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("SWE".ToLower()));break; 
                case "New Zealander": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("NZ".ToLower()));break;
                case "Chilean": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("CHL".ToLower()));break;
                case "Mexican": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("MEX".ToLower()));break;
                case "South African": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("SA".ToLower()));break;
                case "Russian": country= await dbContext.Countries.FirstOrDefaultAsync(x=>x.Code.ToLower().Contains("RUS".ToLower()));break;


                default:Console.WriteLine("Error");break;

            };

            return country;
        }
    }
}