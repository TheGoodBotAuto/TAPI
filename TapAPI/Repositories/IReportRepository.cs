using System;
using System.Collections.Generic;
using TapAPI.Models;
namespace TapAPI.Repositories
{
    public interface IReportRepository
    {
        List<ReportWithDecision> GetAllReports(int ReportType, int PageNumber, int MaxItems);
        List<ReportOverview> GetFindingsCount(int ReportType);
        List<ReportWithDecision> GetServerReport(string ipAddress, int ReportType, int PageNumber, int MaxItems);
        List<ReportType> GetUserRoles(string user);
        bool isAllowed(string user, int reportID);
        bool uploadResponses(string user,UDArray[] decisions);
    }
}
