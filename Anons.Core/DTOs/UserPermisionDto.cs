using Anons.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public class UserPermisionDto : BaseDto
    {
        public int UserId { get; set; }
        public int PermisionId { get; set; }
    }
}
