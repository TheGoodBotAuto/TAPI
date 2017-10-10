using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TapAPI.Models;

namespace TapAPI.Repositories
{
    public class ServerRepository: IServerRepository
    {
		private readonly TapoutDataContext _db;

		public ServerRepository(TapoutDataContext db)
        {
            this._db = db;
        }

        public List<Server> GetServerSummary(int ReportType, int PageNumber, int MaxItems)
        {
			if (MaxItems == 0)
			{
				return _db.ServerSummary.Where(t => t.ReportNameID == ReportType).ToList();
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
				return _db.ServerSummary.Where(t => t.ReportNameID == ReportType ).Skip(MaxItems * (PageNumber - 1)).Take(MaxItems).ToList();
			}
        }

        public Server GetServerSummary(string ID){
            return _db.ServerSummary.Where(t => t.ID == ID).ToList()[0];
        }

        public List<ReportWithDecision> GetServerDetails(int ReportType,string serverID, int PageNumber, int MaxItems){
			if (MaxItems == 0)
			{
				return _db.ReportWithDecision.Where(t => t.ReportNameID == ReportType && t.IPAddress == serverID).ToList();
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
				return _db.ReportWithDecision.Where(t => t.ReportNameID == ReportType && t.Status != "Closed" && t.IPAddress == serverID).Skip(MaxItems * (PageNumber - 1)).Take(MaxItems).ToList();
			}

		}

		public List<ReportOverview> GetFindingsCount(int ReportType)
		{
			return _db.TotalOpenFindings.Where(t => t.ReportNameID == ReportType).ToList();

		}
		public List<ReportType> GetUserRoles(string user)
		{
			return _db.GetUserRoles.Where(t => t.UserName == user).ToList();
		}
		public bool isAllowed(string user, int reportID)
		{
			List<ReportType> allowed = _db.GetUserRoles.Where(t => t.UserName == user && t.ReportNameID == reportID).ToList();
			if (allowed.Count() > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool uploadResponses(string user, ServerResponse[] decisions)
		{
            DateTime date = DateTime.Today;
			foreach (ServerResponse serverRow in decisions)
            {
                Server server = _db.ServerSummary.Where(t => t.ID == serverRow.ID).ToList().ElementAt(0);
                int reportID = server.ReportNameID;


				//Report rep = _db.Report.Find(row.UniqueID);
                IQueryable<Report> reports = _db.Report.Where(t => t.IPAddress==serverRow.IPAddress && t.ReportNameID==reportID && t.Status!="Closed");
               
                foreach (Report row in reports){
					row.Status = "Updated(" + date.ToString("MM/dd/yyyy") + ")";
					Response resp = new Response();
					resp.UserID = user;
					resp.PluginUniqueID = row.UniqueID;
					resp.Decision = serverRow.Decision;
					resp.Comment = serverRow.Comments;
					resp.Date = date;
					//Add response insert here insert into [dbo].[Response] values(@UserID,@UniqueID,@Decision,@Comment,@Date)
					_db.Response.Add(resp);
                }
			}
			int res = _db.SaveChanges();

			if (res > 0)
			{
				return true;
			}
			else
			{
				return false;
			}

		}
    }
}
