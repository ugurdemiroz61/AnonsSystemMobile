using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public class BelediyeDto:BaseDto
    {
        public string BelediyeAdi { get; set; }
        public int UstBelediyeId { get; set; }
    }
}
