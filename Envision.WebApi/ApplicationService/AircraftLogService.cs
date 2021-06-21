using Envision.DAL;
using Envision.DAL.Models;
using Envision.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Envision.WebApi.ApplicationService
{
    public class AircraftLogService
    {
        protected DatabaseContext _context;
        private IAircraftLogService _aircraftLogService;
        public AircraftLogService(DatabaseContext context, IAircraftLogService aircraftLogService)
        {
            _context = context;
            _aircraftLogService = aircraftLogService;
        }

        //public List<AircraftLog> GetAircraftLogs()
        //{

        //}
    }
}
