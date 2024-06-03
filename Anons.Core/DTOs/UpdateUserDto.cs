using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BelediyeId { get; set; }

        [JsonIgnore]
        public string CurrentUserName { get; set; }
    }
}
