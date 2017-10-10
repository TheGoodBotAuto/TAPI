using System;
namespace TapClientAngular.Models
{
    public class AppSettings
    {
		string APIURL { get; set; }
		string IdentityURL { get; set; }
		string ClientURL { get; set; }
        public AppSettings()
        {
        }
    }
}
