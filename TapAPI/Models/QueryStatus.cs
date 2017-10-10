using System;

using System.ComponentModel.DataAnnotations;
namespace TapAPI.Models
{
    public partial class QueryStatus
    {
        [Key]
        public string UniqueID { get; set; }
        public bool result { get; set; }
        public string message { get; set; }

        public QueryStatus()
        {
        }
    }
}
