using Envision.DAL;
using Envision.DAL.Models;
using Envision.WebApi.Models;
using Envision.WebApi.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Envision.WebApi.Repositories
{
    public class AircraftLogRepository : IAircraftLogService
    {
        protected DatabaseContext _context;

        public AircraftLogRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<AircraftLog> GetAircraftLogs()
        {
            List<AircraftLog> aircraftLogList = new List<AircraftLog>();         

            aircraftLogList = (from a in _context.AircraftLogs
                               where a.IsActive == true
                               select a).ToList();

            return aircraftLogList;
        }

        public List<AircraftLog> FilterAircraftLogs(string make, string model, string registration)
        {
            List<AircraftLog> aircraftLogList = new List<AircraftLog>();

            //aircraftLogList = (from a in _context.AircraftLogs
            //                   where a.IsActive == true &&
            //                   a.Make.Contains(make) ||
            //                   a.Model.Contains(model) ||
            //                   a.Registration.Contains(registration)
            //                   select a).ToList();

            aircraftLogList = (from a in _context.AircraftLogs
                               where a.IsActive == true
                               select a).ToList();

            if (make != null)
                aircraftLogList = aircraftLogList.Where(x => x.Make.Contains(make)).ToList();

            if (model != null)
                aircraftLogList = aircraftLogList.Where(x => x.Model.Contains(model)).ToList();

            if (registration != null)
                aircraftLogList = aircraftLogList.Where(x => x.Registration.Contains(registration)).ToList();

            return aircraftLogList;
        }

        public AircraftLog GetAircraftLogById(int id)
        {
            AircraftLog aircraftLog = new AircraftLog();

            aircraftLog = (from a in _context.AircraftLogs
                           join i in _context.AircraftLogImages on a.Id equals i.AircraftLogId
                           where a.Id == id && i.IsActive == true
                           select new AircraftLog
                           {
                               Id = a.Id,
                               Make = a.Make,
                               Model = a.Model,
                               Registration = a.Registration,
                               Location = a.Location,
                               DateTime = a.DateTime,
                               IsActive = a.IsActive,
                               CreateBy = a.CreateBy,
                               CreateDate = a.CreateDate,
                               ModifiedBy = a.ModifiedBy,
                               ModifiedDate = a.ModifiedDate,
                               ImageName = i.ImageName.ToString(),
                               ImageExtension = i.ImageExtension
                           }).FirstOrDefault();

            aircraftLog.Base64image = ReadAircraftImage(aircraftLog.ImageName);

            return aircraftLog;
        }

        public string InsertAircraftLog(AircraftLogVM aircraftLog)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    AircraftLog saveAircraftModel = PrepareAircraftLogInsertParameter(aircraftLog);
                    _context.AircraftLogs.Add(saveAircraftModel);
                    _context.SaveChanges();

                    Guid newFileName = Guid.NewGuid();
                    AircraftLogImage saveAircraftImageModel = PrepareAircraftLogImageInsertParameter(saveAircraftModel.Id, aircraftLog.base64image, newFileName, aircraftLog.CreateBy);
                    _context.AircraftLogImages.Add(saveAircraftImageModel);
                    _context.SaveChanges();

                    UploadAircraftImage(aircraftLog.base64image, newFileName);

                    transaction.Commit();

                    return "Successful";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }

        public string UpdateAircraftLog(AircraftLogVM aircraftLog)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    AircraftLog updateData = PrepareAircraftLogUpdateParameter(aircraftLog);
                    _context.AircraftLogs.Update(updateData);

                    if(aircraftLog.base64image != "")
                    {
                        AircraftLogImage updateAircraftImageModel = PrepareAircraftLogImageUpdateParameter(aircraftLog.Id);
                        _context.AircraftLogImages.Update(updateAircraftImageModel);

                        Guid newFileName = Guid.NewGuid();
                        AircraftLogImage saveAircraftImageModel = PrepareAircraftLogImageInsertParameter(aircraftLog.Id, aircraftLog.base64image, newFileName, aircraftLog.ModifiedBy);
                        _context.AircraftLogImages.Add(saveAircraftImageModel);

                        _context.SaveChanges();

                        UploadAircraftImage(aircraftLog.base64image, newFileName);
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    return "Successful";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }

        public string DeleteAircraftLog(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    AircraftLog deleteData = PrepareAircraftLogDeleteParameter(id);

                    _context.AircraftLogs.Update(deleteData);
                    _context.SaveChanges();
                    transaction.Commit();

                    return "Successful";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.Message;
                }
            }
        }


        #region Private
        private AircraftLog PrepareAircraftLogInsertParameter(AircraftLogVM aircraftLog)
        {
            AircraftLog model = new AircraftLog();
            model.Make = aircraftLog.Make;
            model.Model = aircraftLog.Model;
            model.Registration = aircraftLog.Registration;
            model.Location = aircraftLog.Location;
            model.DateTime = Convert.ToDateTime(aircraftLog.SeenDateTime);
            model.IsActive = true;
            model.CreateBy = aircraftLog.CreateBy;
            model.CreateDate = DateTime.Now;
            return model;
        }

        private AircraftLogImage PrepareAircraftLogImageInsertParameter(int aircraftLogId, string base64image, Guid newFileName, int createBy)
        {
            string[] base64Code = base64image.Split(",");
            string extensionString = base64Code[0].Split("/").Last();
            string extension = extensionString.Split(";").First();

            AircraftLogImage model = new AircraftLogImage();
            model.AircraftLogId = aircraftLogId;
            model.ImageName = newFileName;
            model.ImageExtension = extension;
            model.IsActive = true;
            model.UploadBy = createBy;
            model.UploadDate = DateTime.Now;
            return model;
        }
        

        private AircraftLog PrepareAircraftLogUpdateParameter(AircraftLogVM aircraftLog)
        {
            AircraftLog model = _context.AircraftLogs.Find(aircraftLog.Id);
            if (model != null)
            {
                model.Make = aircraftLog.Make;
                model.Model = aircraftLog.Model;
                model.Registration = aircraftLog.Registration;
                model.Location = aircraftLog.Location;
                model.DateTime = Convert.ToDateTime(aircraftLog.SeenDateTime);
                model.ModifiedBy = aircraftLog.ModifiedBy;
                model.ModifiedDate = DateTime.Now;
            }
            return model;
        }

        private AircraftLogImage PrepareAircraftLogImageUpdateParameter(int aircraftLogId)
        {
            AircraftLogImage model = (from i in _context.AircraftLogImages
                                      where i.AircraftLogId == aircraftLogId && i.IsActive == true
                                      select i).FirstOrDefault();
            if (model != null)
            {
                model.IsActive = false;
            }
            return model;

        }


        private AircraftLog PrepareAircraftLogDeleteParameter(int id)
        {
            AircraftLog model = _context.AircraftLogs.Find(id);
            if(model != null)
            {
                model.IsActive = false;
            }
            return model;
        }


        private bool UploadAircraftImage(string base64image, Guid newFileName)
        {
            var folderName = Path.Combine("Resources", "UploadImages");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            string renamedFile = Convert.ToString(newFileName) + "." + "";
            var fullPath = Path.Combine(pathToSave, renamedFile);
            //var dbPath = Path.Combine(folderName, renamedFile);

            string[] base64Code = base64image.Split(",");
            byte[] bytes = Convert.FromBase64String(base64Code[1]);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image pic = Image.FromStream(ms);
                pic.Save(fullPath);
            }

            return true;
        }

        private string ReadAircraftImage(string fileName)
        {
            var folderName = Path.Combine("Resources", "UploadImages");
            var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(pathToRead, fileName);

            Byte[] bytes = File.ReadAllBytes(fullPath);
            String file = Convert.ToBase64String(bytes);

            return file;
        }
        #endregion
    }
}
