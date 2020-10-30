using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyHiveService.Models;
using MyHiveService.Services;
using MyHiveService.Test.Utilities;
using NUnit.Framework;

namespace MyHiveService.Test.Services
{
    [TestFixture]
    public class SyncServiceTest
    {
        private MyHiveDbContext _ctx;

        [SetUp]
        public void OneTimeSetUp()
        {
            var options = new DbContextOptionsBuilder<MyHiveDbContext>()
                .UseInMemoryDatabase(databaseName: "SyncServiceTest")
                .Options;
            _ctx = new MyHiveDbContext(options, new MockTenantProvider());

        }

        [Test]
        public void SyncNewHive()
        {
            SyncService service = new SyncService(_ctx, new MockLogger<SyncService>(), MapperFactory.GetMapper());
            Hive syncMe = new Hive()
            {
                clientId = Guid.NewGuid(),
                label = "Test1"
            };
            syncMe = service.syncHive(syncMe);
            Assert.That(syncMe.id, Is.Not.Null);
        }

        [Test]
        public void SyncExisting_ServerNewer()
        {
            Hive newer = new Hive()
            {
                lastModified = new DateTime(2020, 11, 20),
                label = "Newer label",
                clientId = Guid.NewGuid()
            };
            _ctx.Hives.Add(newer);
            _ctx.SaveChanges();

            Hive older = new Hive()
            {
                lastModified = new DateTime(2020, 2, 20),
                label = "Older label",
                clientId = newer.clientId,
                id = newer.id
            };

            SyncService service = new SyncService(_ctx, new MockLogger<SyncService>(), MapperFactory.GetMapper());
            Hive syncResult = service.syncHive(older);
            Assert.That(syncResult.label, Is.EqualTo(newer.label));
        }

        [Test]
        public void SyncExisting_ClientNewer()
        {
            Hive older = new Hive()
            {
                lastModified = new DateTime(2020, 2, 20),
                label = "Newer label",
                clientId = Guid.NewGuid()
            };
            _ctx.Hives.Add(older);
            _ctx.SaveChanges();

            Hive newer = new Hive()
            {
                lastModified = new DateTime(2020, 11, 20),
                label = "Older label",
                clientId = older.clientId,
                id = older.id
            };

            SyncService service = new SyncService(_ctx, new MockLogger<SyncService>(), MapperFactory.GetMapper());
            Hive syncResult = service.syncHive(newer);
            Assert.That(syncResult.label, Is.EqualTo(newer.label));
        }
    }
}