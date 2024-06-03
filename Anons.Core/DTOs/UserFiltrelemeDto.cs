using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public class UserFiltrelemeDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public int BelediyeId { get; set; }
    }
}
