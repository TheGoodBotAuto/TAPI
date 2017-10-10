using System;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Configuration;

namespace TapIdentity
{
	public class Config
	{
		// scopes define the resources in your system
		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
			};
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
                new ApiResource{
                    Name="api1",
                    DisplayName="My API",
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Email, JwtClaimTypes.Id },
                    Scopes =
			        {
				        new Scope()
				        {
					        Name = "api1",
					        DisplayName = "Full access to API 2",
				        }
			        }
                }
			};
		}

		// clients want to access resources (aka scopes)
		public static IEnumerable<Client> GetClients(IConfigurationSection configuration)
		{
			// client credentials client
			return new List<Client>
			{
				new Client
				{
					ClientId = "client",
					AllowedGrantTypes = GrantTypes.ClientCredentials,

					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},
					AllowedScopes = { "api1" }
				},

                // resource owner password grant client
                new Client
				{
					ClientId = "ro.client",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},
					AllowedScopes = { "api1" }
				},

                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = false,

					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},

					RedirectUris = { configuration["ClientURL"]+ "/signin-oidc" },
					PostLogoutRedirectUris = { configuration["ClientURL"]+"/signout-callback-oidc" },

					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"api1"
					},
					AllowOfflineAccess = true
				},
                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
				{
					ClientId = "angular_spa",
 	                ClientName = "Angular 4 Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string> { "openid", "profile", "api1" },
                    RedirectUris = new List<string> { configuration["ClientURL"]+"/auth-callback" },
                    PostLogoutRedirectUris = new List<string> { configuration["ClientURL"] },
                    AllowedCorsOrigins = new List<string> { configuration["ClientURL"] },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent=false
				}
			};
		}
	}
}
