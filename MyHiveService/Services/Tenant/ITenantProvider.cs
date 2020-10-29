using System;
using System.ComponentModel.DataAnnotations;

namespace MyHiveService.Services.Tenant
{
    public interface ITenantProvider
    {
        Guid GetTenantId();
    }
}