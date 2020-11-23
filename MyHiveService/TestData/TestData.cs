using System;
using MyHiveService.Models.DB;

namespace MyHiveService.TestData
{
    public class TestDataBuilder
    {
        //Keep the GUIDs constant so that test data can persist between builds
        private static Guid user1Id = Guid.Parse("abb588ea-5008-4b1a-ab4a-42e1e2679155");
        private static Guid user2Id = Guid.Parse("af4bf219-de4d-4be9-9791-5b56f5caea08");

        public static void fill(MyHiveDbContext ctx)
        {
            createTestUsers(ctx);
            createTestHive(ctx);

            ctx.SaveChanges();
        }

        private static void createTestUsers(MyHiveDbContext ctx)
        {
            ctx.Users.Add(new User()
            {
                id = user1Id,
                username = "test_user_1@gmail.com",
                password = BCrypt.Net.BCrypt.HashPassword("password123")
            });

            ctx.Users.Add(new User()
            {
                id = user2Id,
                username = "test_user_2@gmail.com",
                password = BCrypt.Net.BCrypt.HashPassword("password123")
            });
        }

        private static void createTestHive(MyHiveDbContext ctx)
        {
            Hive hive1 = new Hive()
            {
                tenantId = user1Id,
                label = "Hive 1",
                clientId = Guid.NewGuid()
            };
            ctx.Hives.Add(hive1);

            ctx.HiveInspections.Add(new HiveInspection()
            {
                tenantId = user1Id,
                hiveId = hive1.id,
                date = DateTime.Now - TimeSpan.FromDays(1),
                clientId = Guid.NewGuid(),
            });

            for (int i = 0; i < 2; i++)
            {
                HivePart body = new HivePart()
                {
                    tenantId = user1Id,
                    label = $"Hive Body {i}",
                    type = "Hive Body",
                    clientId = Guid.NewGuid(),
                    dateAdded = DateTime.Now - TimeSpan.FromDays(20),
                    hiveId = hive1.id
                };
                ctx.HiveParts.Add(body);

                ctx.HivePartInspections.Add(new HivePartInspection()
                {
                    tenantId = user1Id,
                    hivePartId = body.id,
                    date = DateTime.Now - TimeSpan.FromDays(1),
                    clientId = Guid.NewGuid(),
                });

                for (int f = 0; f < 10; f++)
                {
                    Frame frame = new Frame()
                    {
                        tenantId = user1Id,
                        label = $"Frame {f}",
                        lastInspected = DateTime.Now - TimeSpan.FromDays(1),
                        hivePartId = body.id,
                        clientId = Guid.NewGuid()
                    };
                    ctx.Frames.Add(frame);

                    ctx.FrameInspections.Add(new FrameInspection()
                    {
                        tenantId = user1Id,
                        frameId = frame.id,
                        date = frame.lastInspected,
                        clientId = Guid.NewGuid(),
                    });
                }
            }

            HivePart super = new HivePart()
            {
                tenantId = user1Id,
                label = $"Hive Super 1",
                type = "Hive Super",
                clientId = Guid.NewGuid(),
                dateAdded = DateTime.Now - TimeSpan.FromDays(5),
                hiveId = hive1.id
            };
            ctx.HiveParts.Add(super);

            ctx.HivePartInspections.Add(new HivePartInspection()
            {
                tenantId = user1Id,
                hivePartId = super.id,
                date = DateTime.Now - TimeSpan.FromDays(1),
                clientId = Guid.NewGuid(),
            });

            for (int f = 0; f < 10; f++)
            {
                Frame frame = new Frame()
                {
                    tenantId = user1Id,
                    label = $"Frame {f}",
                    lastInspected = DateTime.Now - TimeSpan.FromDays(1),
                    hivePartId = super.id,
                    clientId = Guid.NewGuid()
                };
                ctx.Frames.Add(frame);

                ctx.FrameInspections.Add(new FrameInspection()
                {
                    tenantId = user1Id,
                    frameId = frame.id,
                    date = frame.lastInspected,
                    clientId = Guid.NewGuid(),
                });
            }
        }
    }
}