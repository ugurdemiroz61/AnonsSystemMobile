using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Entities
{
    public class Permision : BaseEntity
    {
        public ICollection<UserPermision> UserPermisions { get; set; }
        public string PermisionName { get; set; }

    }
}
