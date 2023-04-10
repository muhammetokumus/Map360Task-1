using Map360Task.Application.Repositories;
using Map360Task.Domain.Entities;
using Map360Task.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map360Task.Persistence.Repositories
{
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        public UserReadRepository(Map360Context context) : base(context)
        {
        }
    }
}
