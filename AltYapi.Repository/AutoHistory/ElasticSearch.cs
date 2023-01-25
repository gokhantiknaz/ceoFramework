using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.Repository.AutoHistory
{
    public static class ElasticSearch
    {
        private static readonly ConnectionSettings connSettings =
         new ConnectionSettings(new Uri("http://localhost:9200/"))
                         .DefaultIndex("log_history");
        //Optionally override the default index for specific types
        //.MapDefaultTypeIndices(m => m
        //.Add(typeof(Log), "log_history"));

        private static readonly ElasticClient elasticClient = new ElasticClient(connSettings);

        public static void CheckExistsAndInsert(ChangeLog log)
        {
            //elasticClient.DeleteIndex("change_log");         
            if (!elasticClient.Indices.Exists("change_log").Exists)
            {
                var indexSettings = new IndexSettings();
                indexSettings.NumberOfReplicas = 1;
                indexSettings.NumberOfShards = 3;


                var createIndexDescriptor = new CreateIndexDescriptor("change_history")
                .Mappings(ms => ms.Map<ChangeLog>(m => m.AutoMap()))
                .InitializeUsing(new IndexState() { Settings = indexSettings })
                .Aliases(a => a.Alias("change_log"));

                var response = elasticClient.Indices.Create(createIndexDescriptor);
            }
            elasticClient.Index<ChangeLog>(log, idx => idx.Index("change_history"));
        }
    }
}
