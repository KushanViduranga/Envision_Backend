using Envision.DAL.Models;
using Envision.WebApi.Common;
using Envision.WebApi.Models;
using Envision.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Envision.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftLogController : ControllerBase
    {
        private IAircraftLogService _aircraftLogService;
        public AircraftLogController(IAircraftLogService aircraftLogService)
        {
            _aircraftLogService = aircraftLogService;
        }

        [HttpGet("aircraft-logs")]
        public Response<List<AircraftLog>> GetAircraftLogs()
        {
            var response = new Response<List<AircraftLog>>();
            try
            {
                response.Result = _aircraftLogService.GetAircraftLogs();
                if (response.Result.Count == 0)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Aircraft Log Data Not Found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpGet("filter-aircraft-logs")]
        public Response<List<AircraftLog>> FilterAircraftLogs(string make, string model, string registration)
        {
            var response = new Response<List<AircraftLog>>();
            try
            {
                response.Result = _aircraftLogService.FilterAircraftLogs(make, model, registration);
                if (response.Result.Count == 0)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Aircraft Log Data Not Found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpGet("by-id/{id}")]
        public Response<AircraftLog> GetAircraftLogById(int id)
        {
            var response = new Response<AircraftLog>();
            try
            {
                response.Result = _aircraftLogService.GetAircraftLogById(id);
                if (response.Result == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Aircraft Data Not Found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost("insert")]
        public Response<string> InsertAircraftLog(AircraftLogVM aircraftLog)
        {
            var response = new Response<string>();
            try
            {
                response.Result = _aircraftLogService.InsertAircraftLog(aircraftLog);
                if (response.Result != "Successful")
                {
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    response.Message = "An error occurred while inserting aircraft log.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPut("update")]
        public Response<string> UpdateAircraftLog(AircraftLogVM aircraftLog)
        {
            var response = new Response<string>();
            try
            {
                response.Result = _aircraftLogService.UpdateAircraftLog(aircraftLog);
                if (response.Result != "Successful")
                {
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    response.Message = "An error occurred while updating aircraft log.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpDelete("delete/{id}")]
        public Response<string> DeleteAircraftLog(int id)
        {
            var response = new Response<string>();
            try
            {
                response.Result = _aircraftLogService.DeleteAircraftLog(id);
                if (response.Result != "Successful")
                {
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    response.Message = "An error occurred while deleting aircraft log.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            return response;
        }
        
    }
}
