using System;
using System.Collections.Generic;
namespace TapAPI.Models
{
    public class ReportWithDecisionResult
    {
		public int count { get; set; }
		public List<ReportWithDecision> results { get; set; }

        public ReportWithDecisionResult()
        {
            
        }
    }
}
