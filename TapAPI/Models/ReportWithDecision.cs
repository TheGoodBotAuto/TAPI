using System;
using System.ComponentModel.DataAnnotations;
namespace TapAPI.Models
{
    public class ReportWithDecision 
    {
		[Key]
		public string UniqueID { get; set; }
		public string PluginID { get; set; }
		public string PluginName { get; set; }
		public string Family { get; set; }
		public string Severity { get; set; }
		public string IPAddress { get; set; }
		public string Port { get; set; }
		public string Exploit { get; set; }
		public string NetBIOSName { get; set; }
		public string CVSS { get; set; }
		public string PatchDate { get; set; }
		public DateTime OriginalReportUploadDate { get; set; }
		public DateTime LastReportUploadDate { get; set; }
		public int ReportNameID { get; set; }
		public string Status { get; set; }
		public string NessusLastScanDate { get; set; }
		public string NessusFirstReported { get; set; }
        public string ReportName { get; set; }
        public string Decision { get; set; }
        public string Comment {get; set;}
        public Nullable<DateTime> Date { get; set; }

        public ReportWithDecision()
        {
        }
    }
}
