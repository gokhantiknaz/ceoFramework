using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceo.Repository.AutoHistory
{
    public class AutoHistory
    {
        //
        // Summary:
        //     Gets or sets the primary key.
        //
        // Value:
        //     The id.
        public int Id { get; set; }

        //
        // Summary:
        //     Gets or sets the source row id.
        //
        // Value:
        //     The source row id.
        public string RowId { get; set; }

        //
        // Summary:
        //     Gets or sets the name of the table.
        //
        // Value:
        //     The name of the table.
        public string TableName { get; set; }

        //
        // Summary:
        //     Gets or sets the json about the changing.
        //
        // Value:
        //     The json about the changing.
        public string Changed { get; set; }

        //
        // Summary:
        //     Gets or sets the change kind.
        //
        // Value:
        //     The change kind.
        public EntityState Kind { get; set; }

        //
        // Summary:
        //     Gets or sets the create time.
        //
        // Value:
        //     The create time.
        public DateTime Created { get; set; } = DateTime.UtcNow;

    }
}
