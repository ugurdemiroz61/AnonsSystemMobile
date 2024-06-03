using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public class DuyuruFiltrelemeDto
    {
        public int BelediyeId { get; set; }
        public int DuyuruTipId { get; set; }
        public DateTime? IlkTarih { get; set; }
        public DateTime? SonTarih { get; set; }
    }
}
