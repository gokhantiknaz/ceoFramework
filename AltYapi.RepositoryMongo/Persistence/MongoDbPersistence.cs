using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.RepositoryMongo.Persistence
{
    public static class MongoDbPersistence
    {
        public static void Configure()
        {
            PeopleMap.Configure();

            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            //BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));

            // Conventions
            var pack = new ConventionPack
                {
                    new IgnoreExtraElementsConvention(true),
                    new IgnoreIfDefaultConvention(true),
                };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }
    }
}
