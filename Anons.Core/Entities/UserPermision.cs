using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Entities
{
    public class UserPermision : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int PermisionId { get; set; }
        public Permision Permision { get; set; }

    }
}
