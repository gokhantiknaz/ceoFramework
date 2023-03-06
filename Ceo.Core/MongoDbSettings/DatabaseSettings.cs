using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.Core.MongoDbSettings
{
    public interface IDatabaseSettings
    {
        //appsettings alanındaki "DatabaseSettings" ayarlarına karşılık gelen propertyler oluşturarak options patterni kullanıyoruz.

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }

    public class DatabaseSettings : IDatabaseSettings
    {
        //Bu propertyleri "Startup" tarafında set edeceğiz.

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }

}
