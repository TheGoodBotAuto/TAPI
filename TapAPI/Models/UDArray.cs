using System;
using System.ComponentModel.DataAnnotations;
namespace TapAPI.Models
{
    public class UDArray
    {
        [Key]
		public string UniqueID { get; set; }
		public string Decision { get; set; }
		public string Comment { get; set; }
        public UDArray()
        {
        }
    }
}
