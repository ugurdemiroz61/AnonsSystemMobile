using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Entities
{
    public class RoleMenu:BaseEntity
    {
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }

    }
}
