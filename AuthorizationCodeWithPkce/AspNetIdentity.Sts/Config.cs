// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AspNetIdentity.Sts
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("serviceapi"),
            };

        public static IEnumerable<Client> Clients =>
                new Client[]
                {
                            // Blazor Standalone Client
                            new Client
                            {
                                ClientId = "blazorwasmapp1",
                                RequireClientSecret = false,
                                ClientName = "Blazor Wasm App",
                                AllowedGrantTypes = GrantTypes.Code,
                                AllowAccessTokensViaBrowser = true,
                                RequirePkce = true,
                                RequireConsent = false,
                                AllowedCorsOrigins = { "http://localhost:5006" },
                                RedirectUris = { "http://localhost:5006/authentication/login-callback" },
                                PostLogoutRedirectUris = { "http://localhost:5006/authentication/logout-callback" },
                                AllowedScopes =
                                {
                                    IdentityServerConstants.StandardScopes.OpenId,
                                    IdentityServerConstants.StandardScopes.Profile,
                                    "serviceapi",
                                },
                            }
                };

        public static IEnumerable<IdentityResource> IdentityResources =>
                                   new IdentityResource[]
                           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                           };
    }
}