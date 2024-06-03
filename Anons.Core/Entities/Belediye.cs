using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Entities
{
    public class Belediye : BaseEntity
    {
        public ICollection<Duyuru> Anonss { get; set; }
        public string BelediyeAdi { get; set; }
        public int UstBelediyeId { get; set; }

    }
}
