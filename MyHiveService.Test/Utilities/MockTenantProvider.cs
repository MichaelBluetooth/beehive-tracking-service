using System;
using MyHiveService.Services.Tenant;

namespace MyHiveService.Test.Utilities
{
    public class MockTenantProvider : ITenantProvider
    {
        public Guid? GetTenantId()
        {
            return null;
        }
    }
}