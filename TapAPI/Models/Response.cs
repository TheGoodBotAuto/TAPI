using System;

using System.ComponentModel.DataAnnotations;
namespace TapAPI.Models
{
	public partial class Response
	{
		[Key]
        public int RespID { get; set; }
        public string UserID { get; set; }
        public string PluginUniqueID { get; set; }
        public string Decision { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
		public Response()
		{
		}
	}
}
