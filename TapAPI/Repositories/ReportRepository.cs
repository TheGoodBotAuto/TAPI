using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TapAPI.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.Data;

namespace TapAPI.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly TapoutDataContext _db;

        public ReportRepository(TapoutDataContext db)
        {
            this._db = db;
        }

        public List<ReportWithDecision> GetAllReports(int ReportType, int PageNumber, int MaxItems)
        {
            if(MaxItems==0){
                return _db.ReportWithDecision.Where(t => t.ReportNameID == ReportType).ToList();
            }
            else{
                if(PageNumber<1){
                    PageNumber = 1;
                }
                if(MaxItems<1){
                    MaxItems = 10;
                }
                return _db.ReportWithDecision.Where(t => t.ReportNameID == ReportType && t.Status!="Closed").Skip(MaxItems * (PageNumber - 1)).Take(MaxItems).ToList();    
            }

        }

		public List<ReportWithDecision> GetServerReport(string ipAddress, int ReportType, int PageNumber, int MaxItems)
		{
			if (MaxItems == 0)
			{
                return _db.ReportWithDecision.Where(t => t.ReportNameID == ReportType && t.IPAddress == ipAddress).ToList();
			}
			else
			{
				if (PageNumber < 1)
				{
					PageNumber = 1;
				}
				if (MaxItems < 1)
				{
					MaxItems = 10;
				}
				return _db.ReportWithDecision.Where(t => t.ReportNameID == ReportType && t.Status != "Closed" && t.IPAddress == ipAddress).Skip(MaxItems * (PageNumber - 1)).Take(MaxItems).ToList();
			}

		}


		public List<ReportOverview> GetFindingsCount(int ReportType){
            return _db.TotalOpenFindings.Where(t => t.ReportNameID == ReportType).ToList();

        }

        public List<ReportType> GetUserRoles(string user){
            return _db.GetUserRoles.Where(t => t.UserName == user).ToList();
        }

        public bool isAllowed(string user, int reportID){
            List<ReportType> allowed = _db.GetUserRoles.Where(t => t.UserName == user && t.ReportNameID==reportID).ToList();
            if (allowed.Count()>0){
                return true;
            }
            else{
                return false;
            }
        }

        public bool uploadResponses(string user,UDArray[] decisions){

            foreach(UDArray row in decisions){
                Report rep = _db.Report.Find(row.UniqueID);
                DateTime date = DateTime.Today;
                if (rep!=null){
                    rep.Status = "Updated(" + date.ToString("MM/dd/yyyy") + ")";
                    Response resp = new Response();
                    resp.UserID = user;
                    resp.PluginUniqueID = rep.UniqueID;
                    resp.Decision = row.Decision;
                    resp.Comment = row.Comment;
                    resp.Date = date;
                    //Add response insert here insert into [dbo].[Response] values(@UserID,@UniqueID,@Decision,@Comment,@Date)
                    _db.Response.Add(resp);
					
                }
            }
            int res = _db.SaveChanges();

            if(res>0){
                return true;
            }
            else{
                return false;
            }

        }
    }
}
