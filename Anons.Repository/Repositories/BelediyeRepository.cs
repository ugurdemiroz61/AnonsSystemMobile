using Anons.Core.Entities;
using Anons.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Repository.Repositories
{
    public class BelediyeRepository : GenericRepository<Belediye>,IBelediyeRepository
    {
        public BelediyeRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
