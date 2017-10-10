using Microsoft.EntityFrameworkCore;

namespace TapAPI.Models
{
	public class TapoutDataContext : DbContext
	{
		public TapoutDataContext(DbContextOptions<TapoutDataContext> options)
			: base(options)
		{
		}


		public DbSet<Server> ServerSummary { get; set; }
        public DbSet<Vulnerability> Vulnerabilities { get; set; }
        public DbSet<ReportWithDecision> ReportWithDecision { get; set; }
        public DbSet<ReportOverview> TotalOpenFindings { get; set; }
        public DbSet<ReportType> GetUserRoles { get; set; }
        public DbSet<UDArray> UDArray { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Response> Response { get; set; }

       
		
	}
}