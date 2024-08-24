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
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        public SliderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
