using Anons.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public class DuyuruDto:BaseDto
    {
        public DateTime DuyuruTarihi { get; set; }
        public int DuyuruTipId { get; set; }
        public string DuyuruIcerik { get; set; }
    }
}
