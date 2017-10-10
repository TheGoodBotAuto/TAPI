using System;
using System.Collections.Generic;
namespace TapAPI.Models
{
    public class ServerResults
    {
		public int count { get; set; }
		public List<Server> results { get; set; }
        public ServerResults()
        {
        }
    }
}
