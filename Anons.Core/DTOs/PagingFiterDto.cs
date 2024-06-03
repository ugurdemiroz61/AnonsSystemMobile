using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public class PagingFilterDto<T>
    {
        public int PageNumber { get; set; }
        public int Limit { get; set; }

        [JsonIgnore]
        public int Skip
        {
            get
            {
                return getSkip();
            }
        }
        private int getSkip()
        {
            if (PageNumber <= 0)
                PageNumber = 1;

            if (Limit <= 0)
                Limit = int.MaxValue;

            if (Limit>1000)
            {
                Limit = 1000;
            }

            var skip = (PageNumber - 1) * Limit;
            return skip;
        }
        public T Filtre { get; set; }
    }
}
