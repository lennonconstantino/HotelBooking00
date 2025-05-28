using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    internal interface IGuestRepository
    {
        Task<Guess> Get(int Id);
        Task<int> Save(IGuestRepository guest);
    }
}
