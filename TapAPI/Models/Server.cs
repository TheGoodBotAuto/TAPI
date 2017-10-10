using System;
namespace TapAPI.Models
{
    public class Server
    {
        public string ID { get; set; }
        public int ReportNameID { get; set; }
        public string ReportName { get; set; }
        public string IPAddress { get; set; }
        public string NetBIOSName { get; set; }
        public int NumberOfFindings { get; set; }
        public string HighestSeverity { get; set; }
        public DateTime OldestFindingDate { get; set; }

        public Server()
        {
        }
    }
}
