using Envision.DAL.Models;
using Envision.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Envision.WebApi.Services
{
    public interface IAircraftLogService
    {
        List<AircraftLog> GetAircraftLogs();
        List<AircraftLog> FilterAircraftLogs(string make, string model, string registration);
        AircraftLog GetAircraftLogById(int id);
        string InsertAircraftLog(AircraftLogVM aircraftLog);
        string UpdateAircraftLog(AircraftLogVM aircraftLog);
        string DeleteAircraftLog(int id);
    }
}
