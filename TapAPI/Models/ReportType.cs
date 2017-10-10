using System;
using System.ComponentModel.DataAnnotations;
namespace TapAPI.Models
{
	public class ReportType
	{
        [Key]
        public string ID { get; set; }
		public string UserName { get; set; }
		public string ReportName { get; set; }
        public int ReportNameID { get; set; }

		public ReportType()
		{
		}
	}
}
