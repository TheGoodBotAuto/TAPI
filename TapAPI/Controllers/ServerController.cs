using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TapAPI.Repositories;
using TapAPI.Models;
using IdentityServer4.AccessTokenValidation;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

namespace TapAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly IServerRepository _reports;
        public ServerController(IServerRepository reports)
        {
            this._reports = reports;
        }

        [HttpGet("{id}")]
        public IActionResult GetAllOrders(int id,int pagenum = 0, int maxitems = 0)
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
				if (_reports.isAllowed(name, id))
				{
					ServerResults result = new ServerResults();
					result.count = _reports.GetFindingsCount(id)[0].TotalServers;
					result.results = _reports.GetServerSummary(id, pagenum, maxitems);
					return Ok(result);
				}
				else
				{
					return Unauthorized();
				}
			}
			else
			{
				return BadRequest();
			}

        }

        [HttpGet("[action]/{id}")]
        public IActionResult details(string id, int pagenum = 0, int maxitems = 0)
        {
            String name = "";
            foreach(Claim c in User.Claims){
                if(c.Type=="name"){
                    name = c.Value;
                    break;
                }
            }
            if(name!=""){
                Server serverInfo = _reports.GetServerSummary(id);
                if(_reports.isAllowed(name, serverInfo.ReportNameID)){
					ReportWithDecisionResult result = new ReportWithDecisionResult();
					result.count = serverInfo.NumberOfFindings;
					result.results = _reports.GetServerDetails(serverInfo.ReportNameID, serverInfo.IPAddress, pagenum, maxitems);
					return Ok(result);
                }
                else{
                    return Unauthorized();
                }
            }
            else{
                return BadRequest();
            }

        }

		[HttpPost("")]
		public IActionResult Update([FromBody] ServerResponse[] decisions)
		{
			if (decisions.Length == 0)
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
				else if (c.Type == "sub")
				{
					id = c.Value;
				}
			}

			bool successful = _reports.uploadResponses(id, decisions);
			if (successful)
			{
				return Ok(decisions);
			}
			else
			{
				return Ok("failed");
			}

			//return new NoContentResult();
		}

        
    }
}
