using System;
namespace TapAPI.Models
{
    public class ServerResponse
    {
        public string ID { get; set; }
        public string IPAddress { get; set; }
        public string Decision { get; set; }
        public string Comments { get; set; }

        public ServerResponse()
        {
        }
    }
}
