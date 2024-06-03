using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Anons.Core.DTOs
{
    public abstract class BaseDto
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string CurrentUserName { get; set; }
    }
}
