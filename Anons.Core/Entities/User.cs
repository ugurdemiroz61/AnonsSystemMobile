using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Entities
{
    public class User : IdentityUser
    {
        public int BelediyeId { get; set; }
        public Belediye Belediye { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdateUser { get; set; }

    }
}
