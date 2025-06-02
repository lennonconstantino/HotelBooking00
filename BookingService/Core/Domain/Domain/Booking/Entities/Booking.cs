using Domain.Enums;
using Action = Domain.Enums.Action;

namespace Domain.Booking.Entities
{
    public class Booking
    {
        public Booking() 
        {
            Status = Status.Created;
        }
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Status Status { get; set; }
        public Room.Entities.Room Room { get; set; }
        public Guest.Entities.Guest Guess { get; set; }
        public Status CurrentStatus { get { return Status; } }
        public void ChangeState(Action action) {
            Status = (Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paid,
                (Status.Created, Action.Cancel) => Status.Canceled,
                (Status.Paid, Action.Finish) => Status.Finished,
                (Status.Paid, Action.Refound) => Status.Refounded,
                (Status.Canceled, Action.Reopen) => Status.Canceled,
                _ => Status
            };
        }
    }
}
