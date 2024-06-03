using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Entities
{
    public class Duyuru : BaseEntity
    {
        public DateTime DuyuruTarihi { get; set; }
        public int DuyuruTipId { get; set; }
        public DuyuruTip DuyuruTip { get; set; }
        public string DuyuruIcerik { get; set; }
        public int BelediyeId { get; set; }
        public Belediye Belediye { get; set; }
    }
}
