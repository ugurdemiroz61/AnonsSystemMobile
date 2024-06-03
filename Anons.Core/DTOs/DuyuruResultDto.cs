using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public class DuyuruResultDto : BaseDto
    {
        public DateTime DuyuruTarihi { get; set; }
        public int DuyuruTipId { get; set; }
        public string DuyuruTipAdi { get; set; }
        public string DuyuruIcerik { get; set; }
        public int BelediyeId { get; set; }
        public string BelediyeAdi { get; set; }
    }
}
