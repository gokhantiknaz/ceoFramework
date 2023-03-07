using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceo.Core.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string MachineName { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        [Column(TypeName = "varchar(4000)")]
        public string Message { get; set; }
        [Column(TypeName = "varchar(4000)")]
        public string Logger { get; set; }
        [Column(TypeName = "varchar(4000)")]
        public string Callsite { get; set; }
        [Column(TypeName = "varchar(4000)")]
        public string Exception { get; set; }

    }
}
