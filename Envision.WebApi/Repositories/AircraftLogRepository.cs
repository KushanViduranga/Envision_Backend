using Dapper;
using Envision.DAL.Models;
using Envision.WebApi.Common;
using Envision.WebApi.Models;
using Envision.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Envision.WebApi.Repositories
{
    public class AircraftLogRepository : IAircraftLogService
    {
        private const string SP_SELECT_AIRCRAFTLOGS = "[dbo].[Select_AircraftLogs]";
        private const string SP_SELECT_AIRCRAFTLOG_BYID = "[dbo].[Select_AircraftLogById]";
        private const string SP_INSERT_AIRCRAFTLOG = "[dbo].[Insert_AircraftLog]";
        private const string SP_UPDATE_AIRCRAFTLOG = "[dbo].[Update_AircraftLog]";
        private const string SP_DELETE_AIRCRAFTLOG = "[dbo].[Delete_AircraftLog]";


        public List<AircraftLog> GetAircraftLogs()
        {
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                var result = (con.Query<AircraftLog>(SP_SELECT_AIRCRAFTLOGS,
                                commandType: CommandType.StoredProcedure))?.ToList();
                return result;
            }
        }

        public List<AircraftLog> FilterAircraftLogs(string make, string model, string registration)
        {
            //Not Complete
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                var result = (con.Query<AircraftLog>(SP_SELECT_AIRCRAFTLOGS,
                                commandType: CommandType.StoredProcedure))?.ToList();
                return result;
            }
        }

        public AircraftLog GetAircraftLogById(int id)
        {
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                var result = (con.Query<AircraftLog>(SP_SELECT_AIRCRAFTLOG_BYID,
                                param: new { Id = id },
                                commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return result;
            }
        }

        public string InsertAircraftLog(AircraftLogVM aircraftLog)
        {
            using (var con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        var parameters = PrepareAircraftLogInsertParameter(aircraftLog);
                        con.Execute(SP_INSERT_AIRCRAFTLOG,
                        param: parameters,
                        transaction: tran,
                        commandType: CommandType.StoredProcedure);

                        string result = parameters.Get<string>("@OutMsg");
                        if (result == "Successful")
                        {
                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                        }
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return ex.Message;
                    }
                }
            }
        }

        public string UpdateAircraftLog(AircraftLogVM aircraftLog)
        {
            using (var con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        var parameters = PrepareAircraftLogUpdateParameter(aircraftLog);
                        con.Execute(SP_UPDATE_AIRCRAFTLOG,
                        param: parameters,
                        transaction: tran,
                        commandType: CommandType.StoredProcedure);

                        string result = parameters.Get<string>("@OutMsg");
                        if (result == "Successful")
                        {
                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                        }
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return ex.Message;
                    }
                }
            }
        }

        public string DeleteAircraftLog(int id)
        {
            using (var con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        var parameters = PrepareAircraftLogDeleteParameter(id);
                        con.Execute(SP_DELETE_AIRCRAFTLOG,
                        param: parameters,
                        transaction: tran,
                        commandType: CommandType.StoredProcedure);

                        string result = parameters.Get<string>("@OutMsg");
                        if (result == "Successful")
                        {
                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                        }
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return ex.Message;
                    }
                }
            }
        }


        #region Private
        private DynamicParameters PrepareAircraftLogInsertParameter(AircraftLogVM aircraftLog)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Make", aircraftLog.Make);
            parameters.Add("@Model", aircraftLog.Model);
            parameters.Add("@Registration", aircraftLog.Registration);
            parameters.Add("@Location", aircraftLog.Location);
            parameters.Add("@DateTime", Convert.ToDateTime(aircraftLog.DateTime));
            parameters.Add("@CreateBy", aircraftLog.CreateBy);
            parameters.Add("@OutMsg", "", DbType.String, ParameterDirection.Output);
            return parameters;
        }

        private DynamicParameters PrepareAircraftLogUpdateParameter(AircraftLogVM aircraftLog)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", aircraftLog.Id);
            parameters.Add("@Make", aircraftLog.Make);
            parameters.Add("@Model", aircraftLog.Model);
            parameters.Add("@Registration", aircraftLog.Registration);
            parameters.Add("@Location", aircraftLog.Location);
            parameters.Add("@DateTime", Convert.ToDateTime(aircraftLog.DateTime));
            parameters.Add("@ModifiedBy", aircraftLog.ModifiedBy);
            parameters.Add("@OutMsg", "", DbType.String, ParameterDirection.Output);
            return parameters;
        }

        private DynamicParameters PrepareAircraftLogDeleteParameter(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@OutMsg", "", DbType.String, ParameterDirection.Output);
            return parameters;
        }
        #endregion

    }
}
