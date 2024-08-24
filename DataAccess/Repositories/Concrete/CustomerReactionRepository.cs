
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class CustomerReactionRepository : Repository<CustomerReaction>, ICustomerReactionRepository
    {
        public CustomerReactionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
