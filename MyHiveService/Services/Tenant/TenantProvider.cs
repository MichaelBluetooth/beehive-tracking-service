using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MyHiveService.Services.Tenant
{
    public class TenantProvider : ITenantProvider
    {
        public static readonly string TENANT_CLAIM_TYPE = "tenantId";

        private readonly IHttpContextAccessor _accessor;

        public TenantProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public Guid? GetTenantId()
        {
            if (_accessor.HttpContext != null)
            {
                return Guid.Parse(_accessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == TENANT_CLAIM_TYPE)?.Value);
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}