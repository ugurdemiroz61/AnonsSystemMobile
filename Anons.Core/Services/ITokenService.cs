using Anons.Core.DTOs;
using Anons.Core.Entities;
using Anons.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(User userApp);

        ClientTokenDto CreateTokenByClient(Client client);
    }
}
