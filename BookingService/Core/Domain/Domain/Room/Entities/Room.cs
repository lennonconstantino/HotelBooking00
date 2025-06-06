using Domain.Guest.Enums;
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
        public ICollection<Guest.Entities.Booking> Bookings { get; set; }
        public bool IsAvailable {
            get {
                if (this.InMaintenance || this.HasGuest) return false;
                return true;
            }
        }
        public bool HasGuest 
        { 
            get {
                var notAvalableStatuses = new List<Status>()
                {
                    Status.Created,
                    Status.Paid,
                };

                return this.Bookings.Where(
                    b => b.Room.Id == this.Id &&
                    notAvalableStatuses.Contains(b.Status)
                    ).Count() > 0;
            } 
        }

        public void ValidateState() {
            if (string.IsNullOrEmpty(Name))
                throw new InvalidRoomDataException();

            if (this.Price == null || this.Price.Value < 10) 
            {
                throw new InvalidRoomDataException();
            }
        }

        public bool CanBeBooked() 
        {
            try
            {
                this.ValidateState();
            }
            catch(Exception)
            {
                return false;
            }

            if (!this.IsAvailable)
                return false;

            return true;
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
