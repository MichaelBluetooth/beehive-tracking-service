using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using MyHiveService.Profiles;

namespace MyHiveService.Test.Utilities
{
    public class MapperFactory
    {
        public static IMapper GetMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new FrameInspectionProfile());
                cfg.AddProfile(new HiveInspectionProfile());
                cfg.AddProfile(new HivePartInspectionProfile());
                cfg.AddProfile(new HiveProfile());
                // List<Profile> profiles = new List<Profile>();
                // foreach (Type type in Assembly.GetAssembly(typeof(Profile)).GetTypes()
                //             .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Profile))))
                // {
                //     cfg.AddProfile((Profile)Activator.CreateInstance(type));
                // }
            });
            return new Mapper(config);
        }
    }
}