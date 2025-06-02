using Domain.Guest.ValueObjects;
using Domain.Room.Exceptions;
using Domain.Room.Ports;

namespace Domain.Room.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public Price Price { get; set; }
        public bool IsAvailable {
            get {
                if (InMaintenance || HasGuest) return false;
                return true;
            }
        }
       public bool HasGuest { get { return true; } }

        public void ValidateState() {
            if (string.IsNullOrEmpty(Name))
                throw new InvalidRoomDataException();
        }

        public async Task Save(IRoomRepository roomRepository)
        { 
            ValidateState();

            if (Id == 0)
            {
                Id = await roomRepository.Create(this);
            }
            else 
            {
                // TODO: implements update
            }
        }
    }
}
