using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Notes.Identitiy
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("NotesAPI","Web API")
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("NotesWebApi","Web API",
                    new []{JwtClaimTypes.Name})
                {
                    Scopes={ "NotesWebApi"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId ="notes-web-api",
                    ClientName="Notes Web",
                    AllowedGrantTypes=GrantTypes.Code,
                    RequireClientSecret=false,
                    RequirePkce=true,
                    RedirectUris =
                    {
                        "https://..."
                    },
                    AllowedCorsOrigins =
                    {
                        "https://..."
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://..."
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           "NotesWebApi"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
