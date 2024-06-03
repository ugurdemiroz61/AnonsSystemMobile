using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public class RecordResultDto<Records, Summary>
    {
        public Summary subtotals { get; set; }
        public int RecordCount { get; set; }
        public Records records { get; set; }

        public static RecordResultDto<Records, Summary> fill(Records records, int recordCount, Summary summary)
        {
            return new RecordResultDto<Records, Summary> { records = records, RecordCount = recordCount, subtotals = summary };
        }
        public static RecordResultDto<Records, Summary> fill(Records records, int recordCount)
        {
            return new RecordResultDto<Records, Summary> { records = records, RecordCount = recordCount };
        }
    }
}
