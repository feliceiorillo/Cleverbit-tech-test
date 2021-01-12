using Cleverbit_tech_test.Messages;
using Cleverbit_tech_test.Repository;
using Cleverbit_tech_test.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Cleverbit_tech_test.Entities.Database;

namespace Cleverbit_tech_test.Services.Interface
{
    public interface IReadInfoService
    {
        Task<InitializeResponse> InitializeDB(InitializeRequest request);
    }
}

namespace Cleverbit_tech_test.Services.Implementation
{
    public class ReadInfoService : IReadInfoService
    {
        private readonly DataContext _db;
        private readonly ILogger _logger;

        public ReadInfoService(DataContext db, ILogger<IReadInfoService> logger)
        {
            _db = db;
            _logger = logger;
        }


        /// <summary>
        /// This function reads data from the csv files and write them into the database 
        /// Needs to be run once time
        /// </summary>
        public async Task<InitializeResponse> InitializeDB(InitializeRequest request)
        {
            var response = new InitializeResponse();
            try
            {
                #region Validate
                var currentDbRegions = await _db.Regions.ToListAsync();
                var currentDbEmployees = await _db.Employees.ToListAsync();
                if (currentDbRegions.Count > 0 || currentDbEmployees.Count > 0)
                    throw new Exception("Database Already Initialized");
                #endregion

                var currentpath = Directory.GetCurrentDirectory();
                var regionsCsvPath = Path.Combine(currentpath, "CSV", "regions.csv");
                var employeesCsvPath = Path.Combine(currentpath, "CSV", "employees.csv");

                var regions = await File.ReadAllLinesAsync(regionsCsvPath);
                var employees = await File.ReadAllLinesAsync(employeesCsvPath);

                #region ReadRegions
                var listRegions = new List<Regions>();
                foreach (var region in regions)
                {
                    string[] arr = new string[3];
                    //checking if starts with double quotes
                    if (region.StartsWith("\""))
                    {
                        arr[0] = region.Substring(region.IndexOf("\""), region.LastIndexOf("\"") + 1);
                        var ids = region.Replace(arr[0], "");
                        arr[1] = ids.Split(",")[1];
                        arr[2] = ids.Split(",")[2];
                    }
                    else
                    {
                        arr = region.Split(",");
                    }

                    // checking if contains any parent
                    int? idParent = null;
                    if (!string.IsNullOrEmpty(arr[2]))
                        idParent = Convert.ToInt32(arr[2]);

                    //checking if the data is valid, if not valid, skip current region
                    int id = 0;
                    int.TryParse(arr[1], out id);
                    if (id == 0)
                        continue;

                    listRegions.Add(new Regions()
                    {
                        Name = arr[0],
                        Id = id,
                        IdParent = idParent
                    });
                }
                _db.Regions.AddRange(listRegions);
                #endregion

                #region ReadEmployees
                var listEmployees = new List<Employees>();
                foreach(var employee in employees)
                {
                    string[] arr = employee.Split(",");

                    listEmployees.Add(new Employees()
                    {
                        IdRegion = Convert.ToInt32(arr[0]),
                        FirstName = arr[1],
                        LastName = arr[2]
                    });
                }
                _db.Employees.AddRange(listEmployees);
                #endregion
                await _db.SaveChangesAsync();
                response.Message = $"Inserted {listEmployees.Count} Employees and {listRegions} regions";
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.SetException(ex);
                _logger.LogError(ex, ex.Message);
            }

            return response;
        }
    }
}
