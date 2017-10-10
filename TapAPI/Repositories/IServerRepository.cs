using System;
using System.Collections.Generic;
using TapAPI.Models;
namespace TapAPI.Repositories
{
    public interface IServerRepository
    {
		List<Server> GetServerSummary(int ReportType, int PageNumber, int MaxItems);
        List<ReportOverview> GetFindingsCount(int ReportType);
        List<ReportWithDecision> GetServerDetails(int ReportType,string serverID, int PageNumber, int MaxItems);
        Server GetServerSummary(string ID);
        List<ReportType> GetUserRoles(string user);
        bool isAllowed(string user, int reportID);
        bool uploadResponses(string user, ServerResponse[] decisions);

		
    }
}
