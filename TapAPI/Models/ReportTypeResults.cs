using System;
using System.Collections.Generic;
namespace TapAPI.Models
{
	public class ReportTypeResults
	{
		public int count { get; set; }
		public List<ReportType> results { get; set; }
		public ReportTypeResults()
		{
		}
	}
}
