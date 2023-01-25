using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.Repository.AutoHistory
{

    public class ChangeLog
    {
        public string EntityName { get; set; }
        public string PropertyName { get; set; }
        public string PrimeryKey { get; set; }
        public string Changed { get; set; }
        public DateTime DateChanged { get; set; }
        public EntityState State { get; set; }
    }
}
