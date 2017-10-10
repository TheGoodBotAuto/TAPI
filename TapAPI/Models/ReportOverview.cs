using System;
using System.ComponentModel.DataAnnotations;

namespace TapAPI.Models
{
    public class ReportOverview
    {
		[Key]
		public int ReportNameID { get; set; }
		public int TotalFindings { get; set; }
        public int TotalServers { get; set; }
		public string ReportName { get; set; }
        public ReportOverview()
        {
        }
    }
}
