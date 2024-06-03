using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public class ResetPasswordDto
    {
        public string UserName { get; set; }
        public string NewPassword { get; set; }

        [JsonIgnore]
        public string CurrentUserName { get; set; }
    }
}
