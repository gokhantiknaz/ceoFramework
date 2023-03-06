using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ceo.Repository.AutoHistory
{
    public sealed class AutoHistoryOptions
    {
        //
        // Summary:
        //     The json setting for the 'Changed' column
        public JsonSerializerOptions JsonSerializerOptions;

        //
        // Summary:
        //     The shared instance of the AutoHistoryOptions.
        internal static AutoHistoryOptions Instance { get; } = new AutoHistoryOptions();


        //
        // Summary:
        //     The maximum length of the 'Changed' column. null will use default setting 2048
        //     unless ChangedVarcharMax is true in which case the column will be varchar(max).
        //     Default: null.
        public int? ChangedMaxLength { get; set; }

        //
        // Summary:
        //     Set this to true to enforce ChangedMaxLength. If this is false, ChangedMaxLength
        //     will be ignored. Default: true.
        public bool LimitChangedLength { get; set; } = true;


        //
        // Summary:
        //     The max length for the row id column. Default: 50.
        public int RowIdMaxLength { get; set; } = 50;


        //
        // Summary:
        //     The max length for the table column. Default: 128.
        public int TableMaxLength { get; set; } = 128;


        //
        // Summary:
        //     Prevent constructor from being called eternally.
        private AutoHistoryOptions()
        {
        }
    }
}
