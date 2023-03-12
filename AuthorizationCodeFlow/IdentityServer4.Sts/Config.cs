﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer4.Sts
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("serviceapi"),
            };

        public static IEnumerable<Client> GetClients => new Client[]
{
                new Client
                {
                    ClientId = "mvcapp",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "serviceapi" }
                },
};

        public static IEnumerable<IdentityResource> IdentityResources =>
                           new IdentityResource[]
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
           };
    }
}