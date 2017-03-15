// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;

namespace Identity.API.Models.AccountViewModels
{
    public class ConsentViewModel : ConsentInputModel
    {
        public ConsentViewModel(ConsentInputModel model, string returnUrl, AuthorizationRequest request, Client client, IEnumerable<Resources> resources)
        {
            RememberConsent = model?.RememberConsent ?? true;
            ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>();

            ReturnUrl = returnUrl;

            ClientName = client.ClientName;
            ClientUrl = client.ClientUri;
            ClientLogoUrl = client.LogoUri;
            AllowRememberConsent = client.AllowRememberConsent;

            IdentityScopes = resources.OfType<IdentityResource>().Select(x => new ScopeViewModel(x, ScopesConsented.Contains(x.Name) || model == null)).ToArray();
            ResourceScopes = resources.OfType<ApiResource>().Select(x => new ScopeViewModel(x, ScopesConsented.Contains(x.Name) || model == null)).ToArray();
        }

        public string ClientName { get; set; }
        public string ClientUrl { get; set; }
        public string ClientLogoUrl { get; set; }
        public bool AllowRememberConsent { get; set; }

        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }
        public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }
    }

    public class ScopeViewModel
    {
        public ScopeViewModel(IdentityResource resource, bool check)
        {
            Name = resource.Name;
            DisplayName = resource.DisplayName;
            Description = resource.Description;
            Emphasize = resource.Emphasize;
            Required = resource.Required;
            Checked = check || resource.Required;
        }

        public ScopeViewModel(ApiResource resource, bool check)
        {
            Name = resource.Name;
            DisplayName = resource.DisplayName;
            Description = resource.Description;
            Emphasize = resource.Scopes.Any(x => x.Emphasize); //Hack :: Get'er Done...
            Required = resource.Scopes.Any(x => x.Required); //Hack :: Get'er Done...
            Checked = check || resource.Scopes.Any(x => x.Required); //Hack :: Get'er Done...
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Emphasize { get; set; }
        public bool Required { get; set; }
        public bool Checked { get; set; }
    }
}
