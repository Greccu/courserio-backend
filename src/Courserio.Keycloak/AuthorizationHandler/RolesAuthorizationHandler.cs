using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Courserio.KeyCloak.AuthorizationHandler
{
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private readonly string ClientId;
        
        public RolesAuthorizationHandler(IOptions<KeyCloakOptions> options)
        {
            this.ClientId = options.Value.ClientId;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            if (context.User.Identity is {IsAuthenticated: false})
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var validRole = false;
            if (!requirement.AllowedRoles.Any())
            {
                validRole = true;
            }
            else
            {
                // Client Roles
                var value = context.User.FindFirst(x => x.Type == "resource_access")?.Value;
                if (value != null)
                {
                    var clientRolesAsString = JsonSerializer.Deserialize<Dictionary<string, object>>(value)?[ClientId].ToString();
                    if (clientRolesAsString != null)
                    {
                        var clientRoles = JsonSerializer.Deserialize<AuthorizationHelper>(clientRolesAsString);
                        // Realm Roles
                        var json = context.User.FindFirst(x => x.Type == "realm_access")?.Value;
                        if (json != null)
                        {
                            var realmRoles = JsonSerializer.Deserialize<AuthorizationHelper>(json);

                            validRole = requirement.AllowedRoles.Any(role => clientRoles != null && realmRoles != null && (realmRoles.roles.Contains(role) || clientRoles.roles.Contains(role)));
                        }
                    }
                }
            }

            if (validRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
        
    }
}
