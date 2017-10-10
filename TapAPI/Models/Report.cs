using System;

using System.ComponentModel.DataAnnotations;
namespace TapAPI.Models
{
    public partial class Report
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
        public Report()
        {
        }
    }
}
