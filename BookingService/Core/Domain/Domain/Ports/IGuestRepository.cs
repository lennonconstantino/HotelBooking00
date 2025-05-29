
using Domain.Entities;

namespace Domain.Ports
{
    public interface IGuestRepository
    {
        Task<Guess> Get(int Id);
        Task<int> Save(IGuestRepository guest);
    }
}
