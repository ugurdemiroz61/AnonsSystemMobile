using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Entities
{
    public class Role:IdentityRole
    {

        public DateTime CreatedDate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdateUser { get; set; }

    }
}
