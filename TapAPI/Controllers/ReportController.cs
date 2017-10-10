using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TapAPI.Repositories;
using TapAPI.Models;

using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

namespace TapAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReportRepository _reports;
        public ReportController(IReportRepository reports)
        {
            this._reports = reports;
        }

/*		[HttpGet("")]
		public IActionResult GetAllOrders(int pagenum=0, int maxitems=0)
		{
			List<ReportWithDecision> orders = _reports.GetAllReports(13,pagenum,maxitems);
			return Ok(orders);
		}*/

        [HttpGet("{id}")]
		public IActionResult GetAllOrders2(int id, int pagenum = 0, int maxitems = 0)
		{
            ReportWithDecisionResult result = new ReportWithDecisionResult();
            result.count = _reports.GetFindingsCount(id)[0].TotalFindings;
            result.results = _reports.GetAllReports(id, pagenum, maxitems);
			return Ok(result);
		}

		[HttpGet("[action]")]
		public IActionResult UserReportRoles()
		{
			String name = "";
			foreach (Claim c in User.Claims)
			{
				if (c.Type == "name")
				{
					name = c.Value;
					break;
				}
			}
			if (name != "")
			{
                ReportTypeResults results = new ReportTypeResults();
                results.results = _reports.GetUserRoles(name);
                results.count = results.results.Count();
                return Ok(results);
			}
			else
			{
				return BadRequest();
			}

		}
		[HttpPost("")]
        public IActionResult Update( [FromBody] UDArray[] decisions )
		{
            if (decisions.Length==0)
			{
				return BadRequest();
			}

			String name = "";
            String id = "";
			foreach (Claim c in User.Claims)
			{
				if (c.Type == "name")
				{
					name = c.Value;
					
				}
                else if (c.Type == "sub"){
                    id = c.Value;
                }
			}

            bool successful = _reports.uploadResponses(id, decisions);
            if(successful){
                return Ok(decisions);
            }
            else{
                return Ok("failed");
            }

			//return new NoContentResult();
		}
    }
}
