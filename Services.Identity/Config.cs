using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Services.Identity
{
    public class Config
    {
        public Config()
        {
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "Backend API"),
                new ApiResource("apiFeature", "API Feature")
                {
                    Description = "API Feature Resource",
                    UserClaims = new List<string> {"role"},
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "apiFeature.read",
                            DisplayName = "Read access on api.feature",
                            // UserClaims = { "role", "feature" }
                        },
                        new Scope
                        {
                            Name = "apiFeature.write",
                            DisplayName = "Write access on api.feature",
                        }
                    }
                },
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                //new IdentityResource {
                //    Name = "role",
                //    UserClaims = new List<string> {"role"}
                //}
                //new IdentityResource("role", new []
                //{
                //    "feature",
                //    "feature.admin",
                //    "feature.user"
                //})
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client_003",
                    ClientName = "Client - Implicit",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins = { "http://localhost:4200/" },

                    // API Resources that this client has access to
                    AllowedScopes = new List<string>
                    {
                        // IdentityServerConstants.StandardScopes.OpenId,
                        // IdentityServerConstants.StandardScopes.Profile,
                        "api",
                        "apiFeature.read",
                        "apiFeature.write",
                        // "role"
                    }
                },
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "john",
                    Password = "password",
                    Claims = new List<Claim> {
                        // new Claim(JwtClaimTypes.Email, "jane@org.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "jane",
                    Password = "password"
                }
            };
        }

    }
}

//new Client
//{
//    ClientId = "client_001",
//    ClientName = "Client - Credentials Client",

//    AllowedGrantTypes = GrantTypes.ClientCredentials,

//    ClientSecrets =
//    {
//        new Secret("secret".Sha256())
//    },

//    AllowedScopes = { "api" }
//},
//new Client
//{
//    ClientId = "client_002",
//    ClientName = "Client - Resource Owner Password",

//    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

//    ClientSecrets =
//    {
//        new Secret("secret".Sha256())
//    },

//    AllowedScopes = { "api" }
//},
//new Client
//{
//    ClientId = "client_004",
//    ClientName = "Client - Hybrid",
//    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

//    ClientSecrets =
//    {
//        new Secret("secret".Sha256())
//    },

//    RedirectUris = { "http://www.google.com" },
//    PostLogoutRedirectUris = { "http://www.google.com" },

//    AllowedScopes =
//    {
//        IdentityServerConstants.StandardScopes.OpenId,
//        IdentityServerConstants.StandardScopes.Profile,
//        "api"
//    },
//    AllowOfflineAccess = true
//}