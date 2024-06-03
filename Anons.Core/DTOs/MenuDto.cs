using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public class MenuDto:BaseDto
    {
        public int MenuCode { get; set; }
        public string MenuName { get; set; }
        public int TopMenuCode { get; set; }
        public string MenuUrl { get; set; }
    }
}
