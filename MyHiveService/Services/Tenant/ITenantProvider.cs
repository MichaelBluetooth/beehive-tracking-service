using System;

namespace MyHiveService.Services.Tenant
{
    public interface ITenantProvider
    {
        Guid? GetTenantId();
    }
}