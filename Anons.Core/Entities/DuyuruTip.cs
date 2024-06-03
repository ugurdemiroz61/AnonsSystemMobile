using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Entities
{
    public class DuyuruTip : BaseEntity
    {
        public string DuyuruTipAdi { get; set; }
        public ICollection<Duyuru> Duyurus { get; set; }
    }
}
